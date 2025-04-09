import { Component, OnInit, ViewChildren, ElementRef } from '@angular/core';
import { FormBuilder, Validators, FormControlName, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Cliente, TipoDocumento } from '../models/cliente';
import { ClienteService } from '../services/cliente.service';
import { FormBaseComponent } from '../../base-components/form-base.component';

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html'
})
export class EditarComponent extends FormBaseComponent implements OnInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  cliente: Cliente;
  clienteForm: FormGroup;
  errors: any[] = [];

  constructor(private fb: FormBuilder,
    private clienteService: ClienteService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    super();
    this.cliente = this.route.snapshot.data['cliente'];

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

    this.clienteForm.patchValue({
      nome: this.cliente.nome,
      documento: this.cliente.documento,
      tipoDocumento: this.cliente.tipoDocumento,
      dataNascimento: this.cliente.dataNascimento,
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

  ngAfterViewInit(): void {
    this.configurarValidacaoFormularioBase(this.formInputElements, this.clienteForm);
  }

  editarCliente() {
    if (this.clienteForm.dirty && this.clienteForm.valid) {
      this.cliente = Object.assign({}, this.cliente, this.clienteForm.value);

      this.clienteService.atualizarCliente(this.cliente)
        .subscribe({
          next: sucesso => this.processarSucesso(sucesso),
          error: falha => this.processarFalha(falha)
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