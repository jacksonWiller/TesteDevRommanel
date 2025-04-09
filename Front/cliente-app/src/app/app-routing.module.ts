import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { NotfoundComponent } from './demo/components/notfound/notfound.component';
import { AppLayoutComponent } from "./layout/app.layout.component";

@NgModule({
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: '/clientes', pathMatch: 'full' },
            {
                path: '', component: AppLayoutComponent,
                children: [
                    {
                        path: 'clientes',
                        loadChildren: () => import('./cliente/cliente.module')
                          .then(m => m.ClienteModule)
                    },
                ]
            },
            { path: 'notfound', component: NotfoundComponent },
            { path: '**', redirectTo: '/notfound' },
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule {
}