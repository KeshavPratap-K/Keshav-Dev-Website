import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-clipy',
  templateUrl: './clipy.component.html',
  styleUrls: ['./clipy.component.css']
})
export class ClipyComponent implements OnInit {
  isHistoryExpanded = false;
  constructor() { }

  ngOnInit(): void {
    this.isHistoryExpanded = false;
  }

  toggle() {
    console.log("test");
    this.isHistoryExpanded = !this.isHistoryExpanded;
  }

}
