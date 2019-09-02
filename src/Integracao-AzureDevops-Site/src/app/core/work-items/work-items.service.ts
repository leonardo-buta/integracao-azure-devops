import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

export interface FilterContext {
  Tipo: number;
  Titulo: string;
  Order: string;
  Pagina: number;  
}

@Injectable({
  providedIn: 'root'
})
export class WorkItemsService {

  constructor(private httpClient: HttpClient) {}

  ObterWorkItens(context: FilterContext): Observable<any> {
    return this.httpClient.get<any>(`https://localhost:44396/api/AzureWorkItem/ObterListaWorkItens?Titulo=${context.Titulo}&Tipo=${context.Tipo}&Order=${context.Order}&Pagina=${context.Pagina}`);
  } 
}
