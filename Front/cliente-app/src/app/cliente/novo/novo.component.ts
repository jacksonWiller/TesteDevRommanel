import { Component, OnInit, ViewChildren, ElementRef } from '@angular/core';
import { FormBuilder, Validators, FormControlName, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Cliente, TipoDocumento } from '../models/cliente';
import { ClienteService } from '../services/cliente.service';
import { FormBaseComponent } from '../../base-components/form-base.component';

@Component({
  selector: 'app-novo',
  templateUrl: './novo.component.html'
})
export class NovoComponent extends FormBaseComponent implements OnInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  clienteForm: FormGroup;
  cliente: Cliente;
  errors: any[] = [];

  constructor(private fb: FormBuilder,
    private clienteService: ClienteService,
    private router: Router) { 
      super(); 
      this.validationMessages = {
        nome: {
          required: 'Informe o Nome',
          minlength: 'Mínimo de 2 caracteres',
          maxlength: 'Máximo de 200 caracteres'
        },
        documento: {
          required: 'Informe o Documento'
        },
        email: {
          email: 'E-mail em formato inválido'
        }
      };
      
      super.configurarMensagensValidacaoBase(this.validationMessages);
    }

  ngOnInit(): void {
    this.clienteForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(200)]],
      documento: ['', [Validators.required]],
      tipoDocumento: [TipoDocumento.CPF],
      dataNascimento: [''],
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

  ngAfterViewInit(): void {
    this.configurarValidacaoFormularioBase(this.formInputElements, this.clienteForm);
  }

  adicionarCliente() {
    if (this.clienteForm.dirty && this.clienteForm.valid) {
      this.cliente = Object.assign({}, this.cliente, this.clienteForm.value);

      this.clienteService.novoCliente(this.cliente)
        .subscribe({
          next: (sucesso: any) => { this.processarSucesso(sucesso) },
          error: (falha: any) => { this.processarFalha(falha) }
        });
    }
  }

  processarSucesso(response: any) {
    this.clienteForm.reset();
    this.errors = [];
    this.router.navigate(['/clientes/listar']);
  }

  processarFalha(fail: any) {
    this.errors = fail.error.errors || ['Ocorreu um erro ao processar sua solicitação.'];
  }
}