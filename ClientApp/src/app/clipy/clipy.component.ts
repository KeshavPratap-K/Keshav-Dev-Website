import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-clipy',
  templateUrl: './clipy.component.html',
  styleUrls: ['./clipy.component.css']
})
export class ClipyComponent implements OnInit {
  isHistoryExpanded = false;
  clipyText: string | undefined;
  public showDiv2: boolean = true;
  
  btnGrp = {
    copyBtnText: "Copy",
    clearBtnText: "Clear",
    syncBtnText: "Sync",
  }

  clipyHistoryArray: string[] = [];

  constructor() { }

  ngOnInit(): void {
    this.isHistoryExpanded = false;
  }

  toggle() {
    this.isHistoryExpanded = !this.isHistoryExpanded;
  }

  ChangeText(btnClicked: string) {

    switch (btnClicked) {
      case 'Copy': {
        this.changeTextBtn(this.btnGrp.copyBtnText);
        this.btnGrp.copyBtnText = "Copied";
        this.isHistoryExpanded = true;
        if (this.clipyText != null && this.clipyText != "") {
          if (this.clipyHistoryArray.length == 6)
            this.clipyHistoryArray.shift();
          this.clipyHistoryArray.push(this.clipyText);
        }

        break;
      }
      case 'Clear': {
        this.changeTextBtn(this.btnGrp.clearBtnText);
        this.btnGrp.clearBtnText = "Cleared";
        this.clipyText = "";
        break;
      }
      case 'Sync': {
        this.changeTextBtn(this.btnGrp.syncBtnText);
        this.btnGrp.syncBtnText = "Synced";
        break;
      }
      default: {

      }
    }
    
  }

  changeTextBtn(btnOrginalValue: string) {
    setTimeout(() => {
      if (btnOrginalValue === "Copy")
        this.btnGrp.copyBtnText = btnOrginalValue;
      if (btnOrginalValue === "Clear")
        this.btnGrp.clearBtnText = btnOrginalValue;
      if (btnOrginalValue === "Sync")
        this.btnGrp.syncBtnText = btnOrginalValue;
    }, 1500);
  }

}

class Queue<T>{
  _queue: T[];

  constructor(queue?: T[]) {
    this._queue = queue || [];
  }

  enqueue(item: T) {
    this._queue.push(item);
  }

  dequeue(): T | undefined {
    return this._queue.shift();
  }

  clear() {
    this._queue = [];
  }

  get count(): number {
    return this._queue.length;
  }
}
