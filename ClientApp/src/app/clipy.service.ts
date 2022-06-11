import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ClipyClipboardFields } from './clipy';

@Injectable({
  providedIn: 'root'
})
export class ClipyService {

  public clipyHistoryGet: ClipyClipboardFields[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  async getClipyHistoryData() {
    await this.http.get<ClipyClipboardFields[]>(this.baseUrl + 'clipy').subscribe(result => {
      //this.baseUrlValue = baseUrl;
      //this.clipyHistoryGet = result;
      //this.clipyHistoryArray = this.clipyHistoryGet[0].clipboardData;
      //this.isLoading = false;
      this.clipyHistoryGet = result;
    }, error => console.error(error));
  }
}
