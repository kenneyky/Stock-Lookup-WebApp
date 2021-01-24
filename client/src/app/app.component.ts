import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import {Stock} from 'src/app/models/Stock'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
 
  constructor(private http: HttpClient){ }
  stockSymbol: string = "";

  cardTitle : string;
  cardHTML : string;
  isSwalVisible : boolean = false;

  getTicker() {
    this.http.get("https://localhost:5001/stock-api/stocks/" + this.stockSymbol)
      .subscribe((stock : Stock )=> {
        this.isSwalVisible = true;
        this.cardTitle = stock.name;
        this.cardHTML = 
          "<div>" +
          "<b> Current Price: </b>" + stock.currentPrice + "<br>" +
          "<b> Year High: </b>" + stock.yearHigh + "<br>" +
          "<b> Year Low: </b>" + stock.yearLow + "<br>" +
          "<b> Current Price: </b>" + stock.currentPrice + "<br>" +

          "</div>";

    }, error => {
      this.isSwalVisible = true;
      this.cardTitle = "No results for : " + this.stockSymbol;
    });
  }
}
