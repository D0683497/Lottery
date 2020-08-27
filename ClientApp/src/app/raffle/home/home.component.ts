import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';

import * as THREE from 'three';
import * as Trackballcontrols from 'three-trackballcontrols';

import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  ROTATE_TIME = 3000;
  BASE_HEIGHT = 1080;
  TOTAL_CARDS;
  btns = {
    enter: Element,
    lotteryBar: Element
  };
  prizes;
  EACH_COUNT;
  ROW_COUNT = 7;
  COLUMN_COUNT = 17;
  COMPANY;
  HIGHLIGHT_CELL = [];
  Resolution = 1;

  camera;
  scene;
  renderer;
  controls;
  threeDCards = [];
  targets = {
    table: [],
    sphere: []
  };

  selectedCardIndex = [];
  rotate = false;
  basicData = {
    prizes: [], // 獎品資訊
    users: [], // 所有人
    luckyUsers: {}, // 已中獎的人
    leftUsers: [] // 未中獎的人
  };
  interval;
  currentPrizeIndex; // 目前抽的獎項，從最低獎開始抽，直到抽到大獎
  currentPrize;
  isLotting = false; // 是否正在抽獎
  currentLuckys = [];

  constructor(private dataService: DataService, trackballControls: Trackballcontrols) { }

  ngOnInit(): void {
    this.dataService.getTempData().subscribe(data => {
      this.prizes = data.cfgData.prizes;
      this.EACH_COUNT = data.cfgData.EACH_COUNT;
      this.COMPANY = data.cfgData.COMPANY;
      this.HIGHLIGHT_CELL =
      this.basicData.prizes = data.cfgData.prizes;
      this.TOTAL_CARDS =
      this.basicData.leftUsers = data.leftUsers;
      this.basicData.luckyUsers = data.luckyData;
    });
    // 獲取用戶
  }

  createHighlight() {
    let year = new Date().getFullYear() + '';
    let step = 4, xoffset = 1, yoffset = 1, highlight = [];

    year.split('').forEach(n => {
      highlight = highlight.concat(
        NUMBER_MATRIX[n].map(item => {
          return `${item[0] + xoffset}-${item[1] + yoffset}`;
        });
      );
      xoffset += step;
    });

    return highlight;
  }

  initCards(): void {

  }

  setLotteryStatus(status = false): void {
    // isLotting = status;
  }

}

/* https://animate.style */
/* https://threejs.org/docs/index.html#manual/en/introduction/Installation */
/* https://github.com/JonLim/three-trackballcontrols */
/* https://github.com/sole/tween.js */
