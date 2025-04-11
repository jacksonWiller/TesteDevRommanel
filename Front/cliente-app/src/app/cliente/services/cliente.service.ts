import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";

import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";

import { BaseService } from 'src/app/services/base.service';
import { Cliente, ApiResponse, ClientesResponse } from '../models/cliente';

@Injectable()
export class ClienteService extends BaseService {

    constructor(private http: HttpClient) { super() }

    //public UrlServiceV1: string = "http://localhost:8090/api/"; //docker

    public UrlServiceV1: string = "https://localhost:44379/api/"; //local

    obterTodos(pageNumber: number = 1, 
      pageSize: number = 5, 
      orderBy: string = 'Nome',
      filter: string = ''): Observable<ApiResponse<ClientesResponse>> {

    let params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());

    if (orderBy) {
      params = params.set('Order', orderBy);
    }

    if (filter) {
      params = params.set('Filter', filter);
    }

    return this.http
      .get<ApiResponse<ClientesResponse>>(
          `${this.UrlServiceV1}clientes`, 
          { 
              ...super.ObterAuthHeaderJson(), 
              params 
          }
      )
      .pipe(
          catchError(super.serviceError)
      );
    }

    obterPorId(id: string): Observable<Cliente> {
      return this.http
          .get<ApiResponse<Cliente>>(this.UrlServiceV1 + "clientes/" + id)
          .pipe(
              map(response => response.result),
              catchError(super.serviceError)
          );
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
            .put(this.UrlServiceV1 + "clientes/", cliente, super.ObterAuthHeaderJson())
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