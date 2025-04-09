import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Cliente } from '../models/cliente';
import { ClienteService } from '../services/cliente.service';

@Component({
  selector: 'app-excluir',
  templateUrl: './excluir.component.html'
})
export class ExcluirComponent {

  cliente: Cliente;

  constructor(
    private clienteService: ClienteService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.cliente = this.route.snapshot.data['cliente'];
  }

  excluirCliente() {
    this.clienteService.excluirCliente(this.cliente.id)
      .subscribe({
        next: () => {
          this.sucessoExclusao();
        },
        error: (falha) => {
          this.falha(falha);
        }
      });
  }

  sucessoExclusao() {
    this.router.navigate(['/clientes/listar']);
  }

  falha(falha: any) {
    console.error('Erro na exclusão', falha);
  }

  voltar() {
    this.router.navigate(['/clientes/listar']);
  }
}