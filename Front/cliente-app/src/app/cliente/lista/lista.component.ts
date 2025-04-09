import { Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { Cliente, TipoDocumento } from '../models/cliente';
import { ClienteService } from '../services/cliente.service';
import { FormBuilder, Validators, FormControlName, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';;
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

  currentPage: number = 1;
  pageSize: number = 5;
  totalRecords: number = 0;
  sortField: string = 'Nome';
  sortOrder: number = 1;
  filterValue: string = '';
  isPageChangeTriggered: boolean = false;
  excluirClienteDialog: boolean = false;
  clienteSelecionado: Cliente;

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  clienteForm: FormGroup;

  constructor(private fb: FormBuilder,
    private clienteService: ClienteService,
    private router: Router) { }

  visible: boolean = false;

  ObterClientes() {
    console.log('Obtendo clientes...');
    this.clienteService.obterTodos(
      this.currentPage,
      this.pageSize,
      this.sortOrder === 1 ? this.sortField : `-${this.sortField}`,
      this.filterValue
    )
    .subscribe({
      next: (response) => {
        if (response && response.result) {
          this.clientes = response.result.clientes;
          
          if (response.result.pagedInfo) {
            this.totalRecords = response.result.pagedInfo.totalRecords;
            
            // Só atualiza currentPage e pageSize se NÃO for chamado de onPageChange
            if (!this.isPageChangeTriggered) {
              this.currentPage = response.result.pagedInfo.pageNumber;
              this.pageSize = response.result.pagedInfo.pageSize;
            }
            
            console.log('Total de registros:', this.totalRecords);
          } else {
            this.totalRecords = this.clientes.length;
          }
        } else {
          this.errorMessage = 'Nenhum cliente encontrado';
          this.clientes = [];
        }
        
        // Resetar a flag após a conclusão da requisição
        this.isPageChangeTriggered = false;
      },
      error: (error) => {
        this.errorMessage = error;
        this.isPageChangeTriggered = false;
      }
    });
  }

  onPageChange(event: any) {
      this.currentPage = event.page + 1;
      this.pageSize = event.rows;
      this.ObterClientes();
  }

  onSort(event: any) {
    this.sortField = event.field;
    this.sortOrder = event.order;
  }

  onFilter(value: string) {
    this.filterValue = value;
    this.currentPage = 1;
  }

  editarCliente(cliente: Cliente) {
    this.router.navigate(['/clientes/editar', cliente.id]);
    console.log('Editando cliente:', cliente);
  }

  confirmarExclusao(cliente: Cliente) {
    this.clienteSelecionado = cliente;
    this.excluirClienteDialog = true;
  }

  excluirCliente() {
    this.clienteService.excluirCliente(this.clienteSelecionado.id)
      .subscribe({
        next: () => {
          this.excluirClienteDialog = false;
          
          this.ObterClientes();
          
        },
        error: (erro) => {
          console.error('Erro ao excluir cliente', erro);
        }
      });
  }

  ngOnInit(): void {
    this.ObterClientes();
  }
}