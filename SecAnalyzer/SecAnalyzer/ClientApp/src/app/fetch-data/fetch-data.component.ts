import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { apiConfig } from '../app-config';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  result: string;
  httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "text"
    }),
  };
  
  constructor(http: HttpClient) {
    http.get<string>(apiConfig.webApi + '/values').subscribe((result: string) => {
      this.result = result;
    }, (error: { error: any; }) => console.error(error.error));
  }
}
