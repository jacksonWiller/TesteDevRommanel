import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

@NgModule({
    imports: [
        RouterModule.forRoot([
            {
                path: '', component: AppLayoutComponent,
                children: [
                    { path: '', loadChildren: () => import('./cliente/cliente.module').then(m => m.DashboardModule) },
                    {
                        path: 'cliente',
                        loadChildren: () => import('./cliente/cliente.module')
                          .then(m => m.ClienteModule)
                    },
                ]
            },
        ], { scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled', onSameUrlNavigation: 'reload' })
    ],
    exports: [RouterModule]
})
export class AppRoutingModule {
}