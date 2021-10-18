import * as THREE from 'https://cdnjs.cloudflare.com/ajax/libs/three.js/r128/three.module.min.js';

import { TrackballControls } from './TrackballControls.js';
import { CSS3DRenderer, CSS3DObject } from './CSS3DRenderer.js';

import { NUMBER_MATRIX } from "./config.js";

let camera, scene, renderer, controls;
const ROW_COUNT = 7, COLUMN_COUNT = 17;
let random = getRandom(0, ROW_COUNT * COLUMN_COUNT - 1);

const objects = [];
const targets = { table: [], sphere: [], helix: [], grid: [] };
const buttons = {
    start: document.querySelector("#start-button"),
    lottery: document.querySelector("#lottery-button"),
    confirm: document.querySelector("#confirm-button"),
    end: document.querySelector("#end-button")
}
let prizes = [];
const width = document.querySelector('#container').clientWidth;
const height = document.querySelector('#container').clientHeight;
const size = function() {
    if (window.innerWidth >= 1440) {
        return 30;
    }
    else if (window.innerWidth >= 1024) {
        return 40;
    }
    else if (window.innerWidth >= 768) {
        return 50;
    }
    else if (window.innerWidth >= 425) {
        return 65;
    }
    else if (window.innerWidth >= 375) {
        return 75;
    }
    else {
        return 85;
    }
}

init();
animate();

function init() {
    camera = new THREE.PerspectiveCamera(size(),  width / height, 100, 10);
    camera.position.z = 3000;

    scene = new THREE.Scene();

    let position = {
        x: (140 * COLUMN_COUNT - 150) / 2,
        y: (180 * ROW_COUNT - 150) / 2
    };
    let HIGHLIGHT_CELL = createHighlight();

    // table
    for (let i = 0; i < ROW_COUNT; i++) {
        for (let j = 0; j < COLUMN_COUNT; j++) {
            let isHighlight = HIGHLIGHT_CELL.includes(j + '-' + i);
            const element = createCard(isHighlight, `${i}-${j}`);
            const objectCSS = new CSS3DObject( element );
            objectCSS.position.x = Math.random() * 4000 - 2000;
            objectCSS.position.y = Math.random() * 4000 - 2000;
            objectCSS.position.z = Math.random() * 4000 - 2000;
            scene.add(objectCSS);
            objects.push(objectCSS);
            const object = new THREE.Object3D();
            object.position.x = j * 140 - position.x;
            object.position.y = -(i * 180) + position.y;
            targets.table.push(object);
        }
    }

    // sphere
    const vector = new THREE.Vector3();
    for (let i = 0, l = objects.length; i < l; i ++) {
        const phi = Math.acos(- 1 + (2 * i) / l);
        const theta = Math.sqrt(l * Math.PI) * phi;
        const object = new THREE.Object3D();
        object.position.setFromSphericalCoords(800, phi, theta);
        vector.copy( object.position ).multiplyScalar( 2 );
        object.lookAt( vector );
        targets.sphere.push( object );
    }

    // helix
    for (let i = 0, l = objects.length; i < l; i ++ ) {
        const theta = i * 0.175 + Math.PI;
        const y = - (i * 8) + 450;
        const object = new THREE.Object3D();
        object.position.setFromCylindricalCoords(900, theta, y);
        vector.x = object.position.x * 2;
        vector.y = object.position.y;
        vector.z = object.position.z * 2;
        object.lookAt( vector );
        targets.helix.push( object );
    }

    // grid
    for (let i = 0; i < objects.length; i ++) {
        const object = new THREE.Object3D();
        object.position.x = ((i % 5) * 400) - 800;
        object.position.y = (-( Math.floor(i / 5) % 5) * 400) + 800;
        object.position.z = (Math.floor(i / 25)) * 1000 - 2000;
        targets.grid.push( object );
    }

    //

    renderer = new CSS3DRenderer();
    renderer.setSize(width - 40, height - 40);
    document.getElementById('container').appendChild(renderer.domElement);

    //

    controls = new TrackballControls(camera, renderer.domElement);
    controls.minDistance = 500;
    controls.maxDistance = 6000;
    controls.addEventListener('change', render);
    
    bindEvent();

    switchScreen('welcome');
}

function bindPrize() {
    document.querySelectorAll('.media').forEach(function (e) {
        let id = e.id.split('prize-')[1];
        let progress = e.querySelector(`#prize-count-${id}`);
        let last = progress.max;
        let total = progress.value;
        prizes.push({id, last, total});
    });
    prizes.forEach(function (prize, index) {
        if (prize.last === 0) {
            prize.splice(index, 1);
        }
    });
    prizes.every(function (prize) {
        if (prize.total >= prize.last) {
            activePrize(prize.id);
            return false;
        }
    });
}

function activePrize(prizeId) {
    let image = document.querySelector(`#prize-image-${prizeId}`);
    image.classList.remove('is-48x48');
    image.classList.add('is-64x64');
    let name = document.querySelector(`#prize-name-${prizeId}`);
    name.classList.remove('subtitle', 'is-6', 'has-text-grey');
    name.classList.add('title', 'is-5', 'has-text-black');
    let progress = document.querySelector(`#prize-count-${prizeId}`);
    progress.classList.remove('is-info', 'is-small');
    progress.classList.add('is-primary', 'is-medium');
}

function inActivePrize(prizeId) {
    let image = document.querySelector(`#prize-image-${prizeId}`);
    image.classList.remove('is-64x64');
    image.classList.add('is-48x48');
    let name = document.querySelector(`#prize-name-${prizeId}`);
    name.classList.remove('title', 'is-5', 'has-text-black');
    name.classList.add('subtitle', 'is-6', 'has-text-grey');
    let progress = document.querySelector(`#prize-count-${prizeId}`);
    progress.classList.remove('is-primary', 'is-medium');
    progress.classList.add('is-info', 'is-small');
}

function bindEvent() {
    buttons.start.addEventListener('click', function (e) {
        e.stopPropagation();
        removeHighlight();
        switchScreen('start');
        bindPrize();
    });
    buttons.lottery.addEventListener('click', function (e) {
        e.stopPropagation();
        buttons.lottery.classList.add('is-hidden');
        buttons.confirm.classList.add('is-loading');
        buttons.confirm.classList.remove('is-hidden');
        rotateBall().then(() => {
            $.ajax({
                type: 'GET',
                url: `${document.URL}/prize/${prizes[0].id}`,
                dataType: 'json',
                success: function (res) {
                    modalCard(random, res);
                },
                error: function (err) {
                    console.log(err)
                }
            });
            buttons.confirm.classList.remove('is-loading');
        });
    });
    buttons.confirm.addEventListener('click', function (e) {
        e.stopPropagation();
        buttons.lottery.classList.remove('is-hidden');
        buttons.confirm.classList.add('is-hidden');
    });
    buttons.end.addEventListener('click', function (e) {
        e.stopPropagation();
    });
    
    window.addEventListener('resize', onWindowResize);
}

function switchScreen(type) {
    switch (type) {
        case 'welcome':
            buttons.start.classList.remove('is-hidden');
            buttons.lottery.classList.add('is-hidden');
            buttons.confirm.classList.add('is-hidden');
            buttons.end.classList.add('is-hidden');
            transform(targets.table, 2000);
            break;
        default:
            buttons.start.classList.add('is-hidden');
            buttons.lottery.classList.remove('is-hidden');
            buttons.confirm.classList.add('is-hidden');
            buttons.end.classList.add('is-hidden');
            transform(targets.sphere, 2000);
            break;
    }
}

/* 建立卡片 */
function createCard(isHighlight, content) {
    const element = createElement();
    if (isHighlight) {
        element.className = 'notification has-background-pink';
    } else {
        element.className = 'notification has-background-fcu';
    }
    return element;
}

/* 建立元素 */
function createElement(css, text) {
    let dom = document.createElement("div");
    dom.className = css || '';
    dom.innerHTML = text || '';
    return dom;
}

function createHighlight() {
    let year = new Date().getFullYear() + '';
    let step = 4, xoffset = 1, yoffset = 1, highlight = [];
    year.split('').forEach(n => {
        highlight = highlight.concat(
            NUMBER_MATRIX[n].map(item => {
                return `${item[0] + xoffset}-${item[1] + yoffset}`;
            })
        );
        xoffset += step;
    });
    return highlight;
}

function removeHighlight() {
    document.querySelectorAll('.has-background-pink').forEach(node => {
        node.classList.add('has-background-fcu');
        node.classList.remove('has-background-pink');
    });
}

/* 切換型態 */
function transform(targets, duration) {
    TWEEN.removeAll();
    for (let i = 0; i < objects.length; i ++) {
        const object = objects[i];
        const target = targets[i];
        new TWEEN.Tween(object.position)
            .to( { x: target.position.x, y: target.position.y, z: target.position.z }, Math.random() * duration + duration)
            .easing( TWEEN.Easing.Exponential.InOut )
            .start();

        new TWEEN.Tween(object.rotation)
            .to({ x: target.rotation.x, y: target.rotation.y, z: target.rotation.z }, Math.random() * duration + duration)
            .easing(TWEEN.Easing.Exponential.InOut)
            .start();
    }
    new TWEEN.Tween(this)
        .to({}, duration * 2)
        .onUpdate(render)
        .start();
}

function animate() {
    requestAnimationFrame(animate);
    TWEEN.update();
    controls.update();
}

function render() {
    renderer.render(scene, camera);
}

function getRandom(min, max){
    return Math.floor(Math.random()*(max-min+1))+min;
}

function onWindowResize() {
    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();
    renderer.setSize(window.innerWidth, window.innerHeight);
    render();
}

/* 旋轉 */
function rotateBall() {
    return new Promise((resolve, reject) => {
        scene.rotation.y = 0;
        new TWEEN.Tween(scene.rotation)
            .to({y: Math.PI * 8}, 3000)
            .onUpdate(render)
            .easing(TWEEN.Easing.Exponential.InOut)
            .start()
            .onComplete(() => { resolve(); });
    });
}

function modalCard(random, res) {
    let duration = 600;
    let object = objects[random];
    new TWEEN.Tween(object.position)
        .to({ x: 0, y: 0, z: 2200 }, Math.random() * duration + duration)
        .easing(TWEEN.Easing.Exponential.InOut)
        .start();
    new TWEEN.Tween(object.rotation)
        .to({ x: 0, y: 0, z: 0 }, Math.random() * duration + duration)
        .easing(TWEEN.Easing.Exponential.InOut)
        .start();
    object.element.classList.remove('has-background-fcu');
    object.element.classList.add('awarded', 'has-background-pink', 'is-flex', 'is-flex-direction-column', 'is-justify-content-space-evenly');
    res.Claims.forEach(claim => {
        if (claim.Field.Show) {
            if (claim.Field.Security) {
                const strLen = claim.Value.length;
                switch (strLen) {
                    case 1:
                        object.element.appendChild(createElement('has-text-dark', `<p>${claim.Field.Value}: *</p>`));
                        break;
                    case 2:
                        object.element.appendChild(createElement('has-text-dark', `<p>${claim.Field.Value}: ${claim.Value}*</p>`));
                        break;
                    case 3:
                        object.element.appendChild(createElement('has-text-dark', `<p>${claim.Field.Value}: ${claim.Value[0]}*${claim.Value[2]}</p>`));
                        break;
                    default:
                        const len = Math.floor(strLen / 3); /* 前後字串長度 */
                        const center = strLen - (len * 3);
                        object.element.appendChild(createElement('has-text-dark', `<p>${claim.Field.Value}: ${claim.Value.substring(0, len)}${'*'.repeat(center)}${claim.Value.substring(center+len)}</p>`));
                        break;
                }
            } else {
                object.element.appendChild(createElement('has-text-dark', `<p>${claim.Field.Value}: ${claim.Value}</p>`));
            }
        }
    });
    new TWEEN.Tween(this)
        .to({}, duration * 2)
        .onUpdate(render)
        .start()
        .onComplete();
}