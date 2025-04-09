import { Cliente } from './models/cliente';
import { FormGroup } from '@angular/forms';
import { ElementRef } from '@angular/core';

import { FormBaseComponent } from '../base-components/form-base.component';

export abstract class ClienteBaseComponent extends FormBaseComponent {
    
    cliente: Cliente;
    errors: any[] = [];
    clienteForm: FormGroup;

    constructor() {
        super();

        this.validationMessages = {
            nome: {
                required: 'Informe o Nome',
                minlength: 'Mínimo de 2 caracteres',
                maxlength: 'Máximo de 200 caracteres'
            },
            documento: {
                required: 'Informe o Documento',
            },
            tipoDocumento: {
                required: 'Selecione o tipo de documento',
            },
            email: {
                required: 'Informe o Email',
                email: 'Email em formato inválido'
            },
            telefone: {
                required: 'Informe o Telefone',
            },
            cep: {
                required: 'Informe o CEP',
            },
            logradouro: {
                required: 'Informe o Logradouro',
            },
            cidade: {
                required: 'Informe a Cidade',
            },
            estado: {
                required: 'Informe o Estado',
            }
        }

        super.configurarMensagensValidacaoBase(this.validationMessages);
    }

    protected configurarValidacaoFormulario(formInputElements: ElementRef[]) {
        super.configurarValidacaoFormularioBase(formInputElements, this.clienteForm);
    }
}