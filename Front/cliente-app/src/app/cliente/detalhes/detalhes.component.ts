import { Component } from '@angular/core';
import { Cliente } from '../models/cliente';
import { ActivatedRoute, Router } from '@angular/router';
import { ClienteService } from '../services/cliente.service';

@Component({
  selector: 'app-detalhes',
  templateUrl: './detalhes.component.html'
})
export class DetalhesComponent {

  cliente: Cliente;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private clienteService: ClienteService
  ) {
    this.cliente = this.route.snapshot.data['cliente'];
  }

  voltar() {
    this.router.navigate(['/clientes/listar']);
  }

  excluirCliente() {
    if (confirm('Tem certeza que deseja excluir este cliente?')) {
      this.clienteService.excluirCliente(this.cliente.id)
        .subscribe({
          next: () => {
            this.router.navigate(['/clientes/listar']);
          }
        });
    }
  }
}