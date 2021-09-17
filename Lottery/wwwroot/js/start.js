import * as THREE from 'https://cdnjs.cloudflare.com/ajax/libs/three.js/r128/three.module.min.js';

import { TrackballControls } from './TrackballControls.js';
import { CSS3DRenderer, CSS3DObject } from './CSS3DRenderer.js';

import { NUMBER_MATRIX } from "./config.js";

let camera, scene, renderer;
let controls;
const ROW_COUNT = 7;
const COLUMN_COUNT = 17;
let random = getRandom(0, ROW_COUNT * COLUMN_COUNT - 1);

const objects = [];
const targets = { table: [], sphere: [], helix: [], grid: [] };
let prizes = [];

init();
animate();

function init() {

    camera = new THREE.PerspectiveCamera(40, window.innerWidth / window.innerHeight, 1, 10000);
    camera.position.z = 3000;

    scene = new THREE.Scene();

    // table

    let position = {
        x: (140 * COLUMN_COUNT - 20) / 2,
        y: (180 * ROW_COUNT - 20) / 2
    };
    let HIGHLIGHT_CELL = createHighlight();

    for ( let i = 0; i < ROW_COUNT; i++ ) {
        for (let j = 0; j < COLUMN_COUNT; j++) {
            let isHighlight = HIGHLIGHT_CELL.includes(j + '-' + i);
            const element = createCard(isHighlight, `${i}-${j}`);

            const objectCSS = new CSS3DObject( element );
            objectCSS.position.x = Math.random() * 4000 - 2000;
            objectCSS.position.y = Math.random() * 4000 - 2000;
            objectCSS.position.z = Math.random() * 4000 - 2000;
            scene.add( objectCSS );

            objects.push( objectCSS );

            const object = new THREE.Object3D();
            object.position.x = j * 140 - position.x;
            object.position.y = -(i * 180) + position.y;

            targets.table.push( object );
        }
    }

    // sphere
    const vector = new THREE.Vector3();
    for ( let i = 0, l = objects.length; i < l; i ++ ) {
        const phi = Math.acos( - 1 + ( 2 * i ) / l );
        const theta = Math.sqrt( l * Math.PI ) * phi;
        const object = new THREE.Object3D();
        object.position.setFromSphericalCoords( 800, phi, theta );
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
    for ( let i = 0; i < objects.length; i ++ ) {
        const object = new THREE.Object3D();
        object.position.x = ((i % 5) * 400) - 800;
        object.position.y = (-( Math.floor(i / 5) % 5) * 400) + 800;
        object.position.z = (Math.floor(i / 25)) * 1000 - 2000;
        targets.grid.push( object );
    }

    //

    renderer = new CSS3DRenderer();
    renderer.setSize(window.innerWidth, window.innerHeight);
    document.getElementById('container').appendChild(renderer.domElement);

    //

    controls = new TrackballControls(camera, renderer.domElement);
    controls.minDistance = 500;
    controls.maxDistance = 6000;
    controls.addEventListener('change', render);

    bindEvent();

    switchScreen('enter');
}

function switchScreen(type) {
    switch (type) {
        case 'enter':
            document.querySelector('#enter').classList.remove('none');
            document.querySelector('#lottery').classList.add('none');
            transform(targets.table, 2000);
            break;
        default:
            document.querySelector('#enter').classList.add('none');
            document.querySelector('#lottery').classList.remove('none');
            transform(targets.sphere, 2000);
            break;
    }
}

function bindEvent() {
    document.querySelectorAll('.prize-item').forEach(function (e) {
        let id = e.id.split('prize-item-')[1];
        let quantity = document.querySelector(`#prize-count-${id}`).innerText.split(' / ');
        let last = quantity[0];
        let total = quantity[1];
        document.querySelector(`#prize-bar-${id}`).style.width = `${last / total * 100}%`;
        prizes.push({id, last, total});
    });
    document.querySelector(`#prize-item-${prizes[0].id}`).classList.add('shine');
    document.querySelector("#menu").addEventListener("click", function (e) {
        e.stopPropagation();
        let target = e.target.id;
        switch (target) {
            case 'enter':
                removeHighlight();
                switchScreen('lottery');
                break;
            case 'lottery':
                document.querySelector('#lottery').classList.add('button-loading');
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
                    changePrize();
                    document.querySelector('#confirm').classList.remove('none');
                });
                break;
            case 'confirm':
                document.querySelector('#confirm').classList.add('none');
                closeCard(random).then(() => {
                    random = getRandom(0, ROW_COUNT * COLUMN_COUNT - 1);
                    document.querySelector('#lottery').classList.remove('none');
                });
                break;
        }
    })
    window.addEventListener('resize', onWindowResize);
}

/* 建立卡片 */
function createCard(isHighlight, content) {
    const element = createElement();
    if (isHighlight) {
        element.className = 'element lightitem highlight';
    } else {
        element.className = 'element';
    }
    element.style.backgroundColor = 'rgba(0,127,127,0.3)';
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

function addHighlight() {
    document.querySelectorAll('.lightitem').forEach(node => {
        node.classList.add("highlight");
    });
}

function removeHighlight() {
    document.querySelectorAll('.highlight').forEach(node => {
        node.classList.remove('highlight');
    });
}

/* 切換型態 */
function transform(targets, duration) {
    TWEEN.removeAll();
    for (let i = 0; i < objects.length; i ++) {
        const object = objects[ i ];
        const target = targets[ i ];
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

function onWindowResize() {
    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();
    renderer.setSize(window.innerWidth, window.innerHeight);
    render();
}

function animate() {
    requestAnimationFrame(animate);
    TWEEN.update();
    controls.update();
}

function render() {
    renderer.render(scene, camera);
}

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

    object.element.classList.add('prize');
    res.Claims.forEach(claim => {
        if (claim.Field.Show) {
            if (claim.Field.Security) {
                const strLen = claim.Value.length;
                switch (strLen) {
                    case 1:
                        object.element.appendChild(createElement('symbol', `<p>${claim.Field.Value}: *</p>`));
                        break;
                    case 2:
                        object.element.appendChild(createElement('symbol', `<p>${claim.Field.Value}: ${claim.Value}*</p>`));
                        break;
                    case 3:
                        object.element.appendChild(createElement('symbol', `<p>${claim.Field.Value}: ${claim.Value[0]}*${claim.Value[2]}</p>`));
                        break;
                    default:
                        const len = Math.floor(strLen / 3); /* 前後字串長度 */
                        const center = strLen - (len * 3);
                        object.element.appendChild(createElement('symbol', `<p>${claim.Field.Value}: ${claim.Value.substring(0, len)}${'*'.repeat(center)}${claim.Value.substring(center+len)}</p>`));
                        break;
                }
            } else {
                object.element.appendChild(createElement('symbol', `<p>${claim.Field.Value}: ${claim.Value}</p>`));
            }
        }
    });
    document.querySelector('#lottery').innerText = '確定';

    new TWEEN.Tween(this)
        .to({}, duration * 2)
        .onUpdate(render)
        .start()
        .onComplete();
}

function closeCard(random) {
    prizes[0].last -= 1;
    document.querySelector(`#prize-bar-${prizes[0].id}`).style.width = `${prizes[0].last / prizes[0].total * 100}%`;
    document.querySelector(`#prize-count-${prizes[0].id}`).innerHTML = `${prizes[0].last} / ${prizes[0].total}`;
    if (prizes[0].last == 0) {
        document.querySelector(`#prize-item-${prizes[0].id}`).classList.remove('shine');
        document.querySelector(`#prize-item-${prizes[0].id}`).classList.add('done');
        prizes.splice(0, 1);
        document.querySelector(`#prize-item-${prizes[0].id}`).classList.add('shine');
    }

    let duration = 500;
    let object = objects[random];
    let target = targets.sphere[random];
    new TWEEN.Tween(object.position)
        .to({ x: target.position.x, y: target.position.y, z: target.position.z }, Math.random() * duration + duration)
        .easing(TWEEN.Easing.Exponential.InOut)
        .start();
    new TWEEN.Tween(object.rotation)
        .to({ x: target.rotation.x, y: target.rotation.y, z: target.rotation.z }, Math.random() * duration + duration)
        .easing(TWEEN.Easing.Exponential.InOut)
        .start();
    return new Promise((resolve, reject) => {
        new TWEEN.Tween(this)
            .to({}, duration * 2)
            .onUpdate(render)
            .start()
            .onComplete(() => {
                document.querySelectorAll('.symbol').forEach(e => {
                    e.remove();
                });
                object.element.classList.remove("prize");
                document.querySelector('#lottery').innerText = '抽獎';
                resolve();
            });
    });
}

function changePrize() {

}

function getRandom(min, max){
    return Math.floor(Math.random()*(max-min+1))+min;
}