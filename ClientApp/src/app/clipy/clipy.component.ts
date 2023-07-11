import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ClipyService } from '../clipy.service';
import { ClipyClipboardFields } from '../clipy';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-clipy',
  templateUrl: './clipy.component.html',
  styleUrls: ['./clipy.component.css'],
  animations: [
    // Each unique animation requires its own trigger. The first argument of the trigger function is the name
    trigger('rotatedState', [
      state('default', style({ transform: 'rotate(0)' })),
      state('rotated', style({ transform: 'rotate(-180deg)' })),
      transition('rotated => default', animate('100ms ease-out')),
      transition('default => rotated', animate('100ms ease-in'))
    ])
  ],
})
export class ClipyComponent implements OnInit {
  isHistoryExpanded: boolean = true;
  clipyText: string = "";
  public showDiv2: boolean = true;
  public isLoading: boolean = true;
  public clipyHistoryGet: ClipyClipboardFields[] = [];
  state: string = 'default';

  btnGrp = {
    copyBtnText: "Copy",
    clearBtnText: "Clear",
    syncBtnText: "Sync",
    clearHistoryBtnText: "Clear History"
  }

  clipyHistoryArray: string[] = [];

  

  ngOnInit(): void {
    this.isHistoryExpanded = false;
    this.state = 'default';
  }

  constructor(private clipyService: ClipyService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    http.get<ClipyClipboardFields[]>(baseUrl + 'clipy').subscribe(result => {
      clipyService.clipyHistoryGet = result;
      this.clipyHistoryArray = clipyService.clipyHistoryGet[0].clipboardData;
      this.isLoading = false;
    }, error => console.error(error));
    
  }



  toggle() {
    this.isHistoryExpanded = !this.isHistoryExpanded;
    this.state = (this.state === 'default' ? 'rotated' : 'default');
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
        this.clipyService.clipyHistoryGet[0].clipboardData = this.clipyHistoryArray;
        this.btnGrp.syncBtnText = "Syncing"
        let result = this.updateClipy();
        break;
      }

      case 'ClearHistory': {
        this.changeTextBtn(this.btnGrp.clearHistoryBtnText);
        this.btnGrp.clearHistoryBtnText = "Cleared History";
        this.clipyHistoryArray = [];
        this.ChangeText('Sync');
        this.isHistoryExpanded = false;
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
      if (btnOrginalValue === "Clear History")
        this.btnGrp.clearHistoryBtnText = btnOrginalValue;
    }, 1500);
  }

  updateClipy() {
    this.http.post<ClipyClipboardFields[]>(this.baseUrl + 'clipy', this.clipyService.clipyHistoryGet).subscribe({
      next: data => {
        console.log('Sync response', data);
        this.changeTextBtn("Sync");
        this.btnGrp.syncBtnText = "Synced";
      },
      error: error => {
        console.error('There was an error!', error);
        this.changeTextBtn("Sync");
        this.btnGrp.syncBtnText = "failed";
      }
    });
  }

}


