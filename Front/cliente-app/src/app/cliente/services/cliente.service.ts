import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";

import { BaseService } from 'src/app/services/base.service';
import { Cliente, ApiResponse, ClientesResponse } from '../models/cliente';

@Injectable()
export class ClienteService extends BaseService {

    constructor(private http: HttpClient) { super() }

    public UrlServiceV1: string = "http://localhost:5103/api/";

    obterTodos(): Observable<ApiResponse<ClientesResponse>> {
        return this.http
            .get<ApiResponse<ClientesResponse>>(this.UrlServiceV1 + "clientes", super.ObterAuthHeaderJson())
            .pipe(
                catchError(super.serviceError)
            );
    }

    obterPorId(id: string): Observable<Cliente> {
        return this.http
            .get<Cliente>(this.UrlServiceV1 + "clientes/" + id)
            .pipe(catchError(super.serviceError));
    }

    novoCliente(cliente: Cliente): Observable<Cliente> {
        return this.http
            .post(this.UrlServiceV1 + "clientes", cliente, super.ObterHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    atualizarCliente(cliente: Cliente): Observable<Cliente> {
        return this.http
            .put(this.UrlServiceV1 + "clientes/" + cliente.id, cliente, super.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    excluirCliente(id: string): Observable<Cliente> {
        return this.http
            .delete(this.UrlServiceV1 + "clientes/" + id)
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }
}