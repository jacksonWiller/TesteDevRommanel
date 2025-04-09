import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { Router } from '@angular/router';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {

    constructor(private primengConfig: PrimeNGConfig, private router: Router) { }

    ngOnInit() {
        this.primengConfig.ripple = true;
        if (this.router.url === '/') {
            this.router.navigate(['/clientes']);
          }
    }
}
