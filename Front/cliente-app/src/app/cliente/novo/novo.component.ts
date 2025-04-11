import { Component, OnInit, ViewChildren, ElementRef } from '@angular/core';
import { FormBuilder, Validators, FormControlName, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Cliente, TipoDocumento } from '../models/cliente';
import { ClienteService } from '../services/cliente.service';
import { FormBaseComponent } from '../../base-components/form-base.component';
import { MessageService } from 'primeng/api'; 

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
    private router: Router,
    private messageService: MessageService) { 
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
      dataNascimento: ['', [Validators.required]],
      telefone: ['', [Validators.required]],
      email: ['', [Validators.email]],
      cep: ['', [Validators.required]],
      logradouro: ['', [Validators.required]],
      numero: ['', [Validators.required]],
      bairro: ['', [Validators.required]],
      cidade: ['', [Validators.required]],
      estado: ['', [Validators.required]],
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
    else {
      this.clienteForm.markAllAsTouched();
      
      this.errors = [];
      
      // Busca erros em cada campo
      Object.keys(this.clienteForm.controls).forEach(key => {
        const control = this.clienteForm.get(key);
        if (control.errors) {
          // Adiciona cada erro ao console
          console.log(`Erro no campo ${key}:`, control.errors);
          
          // Adiciona mensagens personalizadas ao array de erros
          if (this.validationMessages[key]) {
            Object.keys(control.errors).forEach(errorKey => {
              if (this.validationMessages[key][errorKey]) {
                this.errors.push(this.validationMessages[key][errorKey]);
              }
            });
          }
        }
      });
      
      // Log de erros no console para depuração
      console.log('Formulário inválido:', this.errors);
      console.log('Estado do formulário:', this.clienteForm);
    }
  }

  processarSucesso(response: any) {
    this.clienteForm.reset();
    this.errors = [];
    this.router.navigate(['/clientes']);
  }

  processarFalha(fail: any) {
    console.log('Falha ao adicionar cliente', fail);
    
    this.errors = [];
    
    if (fail && fail.error && Array.isArray(fail.error.errors)) {
      // Extrai as mensagens dos objetos de erro
      this.errors = fail.error.errors.map(error => error.message);
      
      // Exibe cada erro como um toast separado
      this.errors.forEach(mensagem => {
        this.messageService.add({
          severity: 'error',
          summary: 'Erro ao Cadastrar Cliente',
          detail: mensagem,
          life: 5000 // 5 segundos
        });
      });
    } else {
      // Fallback para erro genérico
      this.messageService.add({
        severity: 'error',
        summary: 'Erro',
        detail: 'Ocorreu um erro ao processar sua solicitação.',
        life: 5000
      });
    }
    
    console.log('Mensagens de erro extraídas:', this.errors);
  }
}