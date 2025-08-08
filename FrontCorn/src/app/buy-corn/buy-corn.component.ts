import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-buy-corn',
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './buy-corn.component.html',
  styleUrls: ['./buy-corn.component.css']
})
export class BuyCornComponent {
  clientId = 1;
  productId = 999;
  responseMessage = '';

  constructor(private http: HttpClient) {}

  buyCorn() {
    const payload = {
      clientId: this.clientId,
      productId: this.productId
    };

    this.http.post('http://127.0.0.1:5120/api/Sales', payload, { responseType: 'text' })
      .subscribe({
        next: (res) => {
          this.responseMessage = '✅ Purchase successful: ' + res;
        },
        error: (err) => {
          this.responseMessage = '❌ Error: ' + JSON.stringify(err.error);
        }
      });
  }
}
