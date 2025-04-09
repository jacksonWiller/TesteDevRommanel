import { Component, OnInit, ViewChildren, ElementRef } from '@angular/core';
import { FormBuilder, Validators, FormControlName, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Cliente, TipoDocumento } from '../models/cliente';
import { ClienteService } from '../services/cliente.service';

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html'
})
export class EditarComponent implements OnInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  clienteForm: FormGroup;
  cliente: Cliente;
  errors: any[] = [];
  
  constructor(
    private fb: FormBuilder,
    private clienteService: ClienteService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    
    this.clienteService.obterPorId(id).subscribe({
      next: (response) => {
        this.cliente = response;
        this.preencherFormulario();
      },
      error: (error) => {
        this.errors = error.error.errors || ['Não foi possível carregar o cliente.'];
      }
    });
    
    this.inicializarFormulario();
  }
  
  inicializarFormulario(): void {
    this.clienteForm = this.fb.group({
      id: [''],
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(200)]],
      documento: ['', [Validators.required]],
      tipoDocumento: [0],
      dataNascimento: [null],
      telefone: [''],
      email: ['', [Validators.email]],
      cep: [''],
      logradouro: [''],
      numero: [''],
      bairro: [''],
      cidade: [''],
      estado: [''],
      inscricaoEstadual: [''],
      isento: [false]
    });
  }
  
  preencherFormulario(): void {
    this.clienteForm.patchValue({
      id: this.cliente.id,
      nome: this.cliente.nome,
      documento: this.cliente.documento,
      tipoDocumento: this.cliente.tipoDocumento,
      dataNascimento: this.cliente.dataNascimento ? new Date(this.cliente.dataNascimento) : null,
      telefone: this.cliente.telefone,
      email: this.cliente.email,
      cep: this.cliente.cep,
      logradouro: this.cliente.logradouro,
      numero: this.cliente.numero,
      bairro: this.cliente.bairro,
      cidade: this.cliente.cidade,
      estado: this.cliente.estado,
      inscricaoEstadual: this.cliente.inscricaoEstadual,
      isento: this.cliente.isento
    });
  }

  editarCliente(): void {
    if (this.clienteForm.valid && this.clienteForm.dirty) {
      const clienteAtualizado = { ...this.cliente, ...this.clienteForm.value };
      
      this.clienteService.atualizarCliente(clienteAtualizado).subscribe({
        next: () => {
          this.router.navigate(['/clientes/listar']);
        },
        error: (error) => {
          this.errors = error.error.errors || ['Ocorreu um erro ao atualizar o cliente.'];
        }
      });
    }
  }
}