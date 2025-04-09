import { Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { Cliente, TipoDocumento } from '../models/cliente';
import { ClienteService } from '../services/cliente.service';
import { FormBuilder, Validators, FormControlName, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, fromEvent, merge } from 'rxjs';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-lista',
  templateUrl: './lista.component.html',
  providers: [DialogService]
})
export class ListaComponent implements OnInit {

  public clientes: Cliente[];
  public cliente: Cliente;
  public selectedClientes: Cliente[];
  errorMessage: string;
  ref: DynamicDialogRef | undefined;

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  clienteForm: FormGroup;

  constructor(private fb: FormBuilder,
    private clienteService: ClienteService,
    private dialogService: DialogService,
    private router: Router) { }

  visible: boolean = false;

  showDialog() {
      this.visible = true;
  }

  closeDialog() {
    this.visible = false;
  } 

  ObterClientes() {
    console.log('Obtendo clientes...');
    this.clienteService.obterTodos()
    .subscribe(
      response => {
        if (response && response.result && response.result.clientes) {
          this.clientes = response.result.clientes;
        } else {
          this.errorMessage = 'Nenhum cliente encontrado';
        }
      },
      error => {
        this.errorMessage = error;
      }
    );
  } 

  reloadComponent() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  adicionarCliente() {
    if (this.clienteForm.dirty && this.clienteForm.valid) {
      this.cliente = Object.assign({}, this.cliente, this.clienteForm.value);
      this.clienteService.novoCliente(this.cliente)
        .subscribe({
          next: (sucesso: any) => {
            this.closeDialog();
            this.reloadComponent();
          },
        });
    }
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

    this.ObterClientes();
  }
}