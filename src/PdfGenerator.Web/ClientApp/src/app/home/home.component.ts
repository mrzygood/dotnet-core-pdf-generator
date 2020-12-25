import {Component, Inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {saveAs} from 'file-saver';
import {tap} from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string,) {
    this.baseUrl = baseUrl;
  }

  getCertificate() {
    console.log("click get certificate");
    this.http
      .get(this.baseUrl + 'Certificate/GetCertificate', {responseType: 'blob'})
      .pipe(tap((e) => console.log(e)))
      .toPromise()
      .then(fileAsBlob => {
        saveAs(fileAsBlob, "cert.pdf");
      })
      .catch(console.log);
  }

}
