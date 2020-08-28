import { Component, OnInit, AfterViewInit } from '@angular/core';

// 3D
import * as THREE from 'three';
import { TrackballControls } from 'three/examples/jsm/controls/TrackballControls';
import { CSS3DRenderer, CSS3DObject } from 'three/examples/jsm/renderers/CSS3DRenderer';
// 補足動畫
import { Tween, Easing, removeAll, update } from 'es6-tween';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, AfterViewInit {

  public table: any; // 放資料
  public objects: any = []; // 目前的物體
  public targets: any = { table: [], sphere: [], helix: [], grid: [] }; // 全部物體
  public camera: THREE.PerspectiveCamera; // 相機
  public scene: THREE.Scene; // 場景
  public renderer: CSS3DRenderer; // 渲染方式
  public controls: TrackballControls;

  constructor() { }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    this.table = [
      'H', 'Hydrogen', '1.00794', 1, 1,
      'He', 'Helium', '4.002602', 18, 1,
      'Li', 'Lithium', '6.941', 1, 2,
      'Be', 'Beryllium', '9.012182', 2, 2,
      'B', 'Boron', '10.811', 13, 2,
      'C', 'Carbon', '12.0107', 14, 2,
      'N', 'Nitrogen', '14.0067', 15, 2,
      'O', 'Oxygen', '15.9994', 16, 2,
      'F', 'Fluorine', '18.9984032', 17, 2,
      'Ne', 'Neon', '20.1797', 18, 2,
      'Na', 'Sodium', '22.98976...', 1, 3,
      'Mg', 'Magnesium', '24.305', 2, 3,
      'Al', 'Aluminium', '26.9815386', 13, 3,
      'Si', 'Silicon', '28.0855', 14, 3,
      'P', 'Phosphorus', '30.973762', 15, 3,
      'S', 'Sulfur', '32.065', 16, 3,
      'Cl', 'Chlorine', '35.453', 17, 3,
      'Ar', 'Argon', '39.948', 18, 3,
      'K', 'Potassium', '39.948', 1, 4,
      'Ca', 'Calcium', '40.078', 2, 4,
      'Sc', 'Scandium', '44.955912', 3, 4,
      'Ti', 'Titanium', '47.867', 4, 4,
      'V', 'Vanadium', '50.9415', 5, 4,
      'Cr', 'Chromium', '51.9961', 6, 4,
      'Mn', 'Manganese', '54.938045', 7, 4,
      'Fe', 'Iron', '55.845', 8, 4,
      'Co', 'Cobalt', '58.933195', 9, 4,
      'Ni', 'Nickel', '58.6934', 10, 4,
      'Cu', 'Copper', '63.546', 11, 4,
      'Zn', 'Zinc', '65.38', 12, 4,
      'Ga', 'Gallium', '69.723', 13, 4,
      'Ge', 'Germanium', '72.63', 14, 4,
      'As', 'Arsenic', '74.9216', 15, 4,
      'Se', 'Selenium', '78.96', 16, 4,
      'Br', 'Bromine', '79.904', 17, 4,
      'Kr', 'Krypton', '83.798', 18, 4,
      'Rb', 'Rubidium', '85.4678', 1, 5,
      'Sr', 'Strontium', '87.62', 2, 5,
      'Y', 'Yttrium', '88.90585', 3, 5,
      'Zr', 'Zirconium', '91.224', 4, 5,
      'Nb', 'Niobium', '92.90628', 5, 5,
      'Mo', 'Molybdenum', '95.96', 6, 5,
      'Tc', 'Technetium', '(98)', 7, 5,
      'Ru', 'Ruthenium', '101.07', 8, 5,
      'Rh', 'Rhodium', '102.9055', 9, 5,
      'Pd', 'Palladium', '106.42', 10, 5,
      'Ag', 'Silver', '107.8682', 11, 5,
      'Cd', 'Cadmium', '112.411', 12, 5,
      'In', 'Indium', '114.818', 13, 5,
      'Sn', 'Tin', '118.71', 14, 5,
      'Sb', 'Antimony', '121.76', 15, 5,
      'Te', 'Tellurium', '127.6', 16, 5,
      'I', 'Iodine', '126.90447', 17, 5,
      'Xe', 'Xenon', '131.293', 18, 5,
      'Cs', 'Caesium', '132.9054', 1, 6,
      'Ba', 'Barium', '132.9054', 2, 6,
      'La', 'Lanthanum', '138.90547', 4, 9,
      'Ce', 'Cerium', '140.116', 5, 9,
      'Pr', 'Praseodymium', '140.90765', 6, 9,
      'Nd', 'Neodymium', '144.242', 7, 9,
      'Pm', 'Promethium', '(145)', 8, 9,
      'Sm', 'Samarium', '150.36', 9, 9,
      'Eu', 'Europium', '151.964', 10, 9,
      'Gd', 'Gadolinium', '157.25', 11, 9,
      'Tb', 'Terbium', '158.92535', 12, 9,
      'Dy', 'Dysprosium', '162.5', 13, 9,
      'Ho', 'Holmium', '164.93032', 14, 9,
      'Er', 'Erbium', '167.259', 15, 9,
      'Tm', 'Thulium', '168.93421', 16, 9,
      'Yb', 'Ytterbium', '173.054', 17, 9,
      'Lu', 'Lutetium', '174.9668', 18, 9,
      'Hf', 'Hafnium', '178.49', 4, 6,
      'Ta', 'Tantalum', '180.94788', 5, 6,
      'W', 'Tungsten', '183.84', 6, 6,
      'Re', 'Rhenium', '186.207', 7, 6,
      'Os', 'Osmium', '190.23', 8, 6,
      'Ir', 'Iridium', '192.217', 9, 6,
      'Pt', 'Platinum', '195.084', 10, 6,
      'Au', 'Gold', '196.966569', 11, 6,
      'Hg', 'Mercury', '200.59', 12, 6,
      'Tl', 'Thallium', '204.3833', 13, 6,
      'Pb', 'Lead', '207.2', 14, 6,
      'Bi', 'Bismuth', '208.9804', 15, 6,
      'Po', 'Polonium', '(209)', 16, 6,
      'At', 'Astatine', '(210)', 17, 6,
      'Rn', 'Radon', '(222)', 18, 6,
      'Fr', 'Francium', '(223)', 1, 7,
      'Ra', 'Radium', '(226)', 2, 7,
      'Ac', 'Actinium', '(227)', 4, 10,
      'Th', 'Thorium', '232.03806', 5, 10,
      'Pa', 'Protactinium', '231.0588', 6, 10,
      'U', 'Uranium', '238.02891', 7, 10,
      'Np', 'Neptunium', '(237)', 8, 10,
      'Pu', 'Plutonium', '(244)', 9, 10,
      'Am', 'Americium', '(243)', 10, 10,
      'Cm', 'Curium', '(247)', 11, 10,
      'Bk', 'Berkelium', '(247)', 12, 10,
      'Cf', 'Californium', '(251)', 13, 10,
      'Es', 'Einstenium', '(252)', 14, 10,
      'Fm', 'Fermium', '(257)', 15, 10,
      'Md', 'Mendelevium', '(258)', 16, 10,
      'No', 'Nobelium', '(259)', 17, 10,
      'Lr', 'Lawrencium', '(262)', 18, 10,
      'Rf', 'Rutherfordium', '(267)', 4, 7,
      'Db', 'Dubnium', '(268)', 5, 7,
      'Sg', 'Seaborgium', '(271)', 6, 7,
      'Bh', 'Bohrium', '(272)', 7, 7,
      'Hs', 'Hassium', '(270)', 8, 7,
      'Mt', 'Meitnerium', '(276)', 9, 7,
      'Ds', 'Darmstadium', '(281)', 10, 7,
      'Rg', 'Roentgenium', '(280)', 11, 7,
      'Cn', 'Copernicium', '(285)', 12, 7,
      'Nh', 'Nihonium', '(286)', 13, 7,
      'Fl', 'Flerovium', '(289)', 14, 7,
      'Mc', 'Moscovium', '(290)', 15, 7,
      'Lv', 'Livermorium', '(293)', 16, 7,
      'Ts', 'Tennessine', '(294)', 17, 7,
      'Og', 'Oganesson', '(294)', 18, 7
    ];

    this.init();
    this.animate();
  }

  init(): void {
    const windowsWidth = window.innerWidth;
    const windowsHeight = window.innerHeight;

    this.scene = new THREE.Scene(); // 建立場景
    this.camera = new THREE.PerspectiveCamera(40, windowsWidth / windowsHeight, 1, 10000); // 建立相機(視角, 畫面寬高比, 近面距離, 遠面距離)
    this.camera.position.z = 3000; // 設定相機位置

    // table
    this.initTable();

    // sphere
    this.initSphere();

    // helix
    this.initHelix();

    // grid
    this.initGrid();

    this.renderer = new CSS3DRenderer(); // 建立渲染器
    this.renderer.setSize(windowsWidth, windowsHeight); // 設定場景大小
    document.getElementById('container').appendChild(this.renderer.domElement); // 將渲染器的 DOM 綁到網頁上

    // 讓滑鼠滾輪可以控制縮放
    this.controls = new TrackballControls(this.camera, this.renderer.domElement);
    this.controls.minDistance = 500;
    this.controls.maxDistance = 6000;
    this.controls.addEventListener('change', this.render.bind(this));

    this.transformTable();
    window.addEventListener('resize', this.onWindowResize.bind(this), false);

  }

  transform(targets, duration): void {

    removeAll();

    for (let i = 0; i < this.objects.length; i++) {
      const object = this.objects[i];
      const target = targets[i];

      new Tween(object.position)
        .to({x: target.position.x, y: target.position.y, z: target.position.z}, Math.random() * duration + duration)
        .easing(Easing.Exponential.InOut)
        .start();
      new Tween(object.rotation)
      .to({x: target.rotation.x, y: target.rotation.y, z: target.rotation.z}, Math.random() * duration + duration)
      .easing(Easing.Exponential.InOut)
      .start();
    }

    new Tween(this)
      .to({}, duration * 2)
      .on('update', this.render)
      .start();
  }

  // 視窗大小改變
  onWindowResize(): void {
    this.camera.aspect = window.innerWidth / window.innerHeight;
    this.camera.updateProjectionMatrix();
    this.renderer.setSize(window.innerWidth, window.innerHeight);
    this.render();
  }

  // 動畫
  animate(): void {
    requestAnimationFrame(this.animate.bind(this));
    update();
    this.controls.update();
  }

  // 輸出至畫面上
  render(): void {
    this.renderer.render(this.scene, this.camera);
  }

  initTable(): void {
    for (let i = 0; i < this.table.length; i += 5) {
      const element = document.createElement('div');
      element.className = 'element';
      element.style.backgroundColor = 'rgba(0,127,127,' + (Math.random() * 0.5 + 0.25) + ')';

      const num: any = document.createElement('div');
      num.className = 'number';
      num.textContent = (i / 5) + 1;
      element.appendChild(num);

      const symbol: any = document.createElement('div');
      symbol.className = 'symbol';
      symbol.textContent = this.table[i];
      element.appendChild(symbol);

      const details: any = document.createElement('div');
      details.className = 'details';
      details.innerHTML = this.table[i + 1] + '<br>' + this.table[i + 2];
      element.appendChild(details);

      const object = new CSS3DObject(element);
      object.position.x = Math.random() * 4000 - 2000;
      object.position.y = Math.random() * 4000 - 2000;
      object.position.z = Math.random() * 4000 - 2000;
      this.scene.add(object);

      this.objects.push(object);

      const object3D: any = new THREE.Object3D();
      object3D.position.x = (this.table[i + 3] * 140) - 1330;
      object3D.position.y = - (this.table[i + 4] * 180 ) + 990;

      this.targets.table.push(object3D);
    }
  }

  initSphere(): void {
    const vector = new THREE.Vector3();

    for (let i = 0, l = this.objects.length; i < l; i++) {
      const phi = Math.acos( - 1 + ( 2 * i ) / l );
      const theta = Math.sqrt( l * Math.PI ) * phi;

      const object = new THREE.Object3D();

      object.position.setFromSphericalCoords( 800, phi, theta );

      vector.copy( object.position ).multiplyScalar( 2 );

      object.lookAt( vector );

      this.targets.sphere.push( object );
    }
  }

  initHelix(): void {
    const vector = new THREE.Vector3();

    for (let i = 0, l = this.objects.length; i < l; i++) {
      const theta = i * 0.175 + Math.PI;
      const y = - ( i * 8 ) + 450;

      const object = new THREE.Object3D();

      object.position.setFromCylindricalCoords(900, theta, y);

      vector.x = object.position.x * 2;
      vector.y = object.position.y;
      vector.z = object.position.z * 2;

      object.lookAt(vector);

      this.targets.helix.push(object);
    }
  }

  initGrid(): void {
    for (let i = 0; i < this.objects.length; i ++) {
      const object = new THREE.Object3D();

      object.position.x = ( ( i % 5 ) * 400 ) - 800;
      object.position.y = ( - ( Math.floor( i / 5 ) % 5 ) * 400 ) + 800;
      object.position.z = ( Math.floor( i / 25 ) ) * 1000 - 2000;

      this.targets.grid.push(object);
    }
  }

  transformTable(): void {
    this.transform(this.targets.table, 2000);
  }

  transformSphere(): void {
    this.transform(this.targets.sphere, 2000);
  }

  transformHelix(): void {
    this.transform(this.targets.helix, 2000);
  }

  transformGrid(): void {
    this.transform(this.targets.grid, 2000);
  }

}

/* https://animate.style */

/* https://github.com/JonLim/three-trackballcontrols */


/* 官網 https://threejs.org/ */
/* 範例 demo https://threejs.org/examples/#css3d_periodictable */
/* 範例 code https://github.com/mrdoob/three.js/blob/master/examples/css3d_periodictable.html */
/* https://github.com/JohnnyDevNull/ng-three-template */
/* https://github.com/omidsakhi/ng-three-periodictable */


/* https://github.com/tweenjs/tween.js/ */
/* https://github.com/tweenjs/es6-tween */
