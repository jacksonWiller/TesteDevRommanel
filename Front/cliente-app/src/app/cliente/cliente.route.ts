import { Cliente } from './models/cliente';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClienteAppComponent } from './cliente.app.component';
import { ListaComponent } from './lista/lista.component';


const produtoRouterConfig: Routes = [
    {
        path: '', component: ClienteAppComponent,
        children: [
            { path: 'listar', component: ListaComponent },
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(clienteRouterConfig)
    ],
    exports: [RouterModule]
})
export class ProdutoRoutingModule { }