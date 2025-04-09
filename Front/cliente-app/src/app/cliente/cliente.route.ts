import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClienteAppComponent } from './cliente.app.component';
import { ListaComponent } from './lista/lista.component';
import { NovoComponent } from './novo/novo.component';
import { EditarComponent } from './editar/editar.component';
import { DetalhesComponent } from './detalhes/detalhes.component';
import { ExcluirComponent } from './excluir/excluir.component';
import { ClienteResolve } from './services/cliente.resolve';
import { ClienteGuard } from './services/cliente.guard';

const clienteRouterConfig: Routes = [
    {
        path: '', component: ClienteAppComponent,
        children: [
            { path: '', component: ListaComponent },
            {
                path: 'novo', component: NovoComponent,
            },
            {
                path: 'editar/:id', component: EditarComponent,
                resolve: {
                    cliente: ClienteResolve
                }
            },
            {
                path: 'detalhes/:id', component: DetalhesComponent,
                resolve: {
                    cliente: ClienteResolve
                }
            },
            {
                path: 'excluir/:id', component: ExcluirComponent,
                resolve: {
                    cliente: ClienteResolve
                }
            },
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(clienteRouterConfig)
    ],
    exports: [RouterModule]
})
export class ClienteRoutingModule { }