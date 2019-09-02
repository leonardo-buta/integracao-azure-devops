import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
// import {
//   WorkItemService,
//   FilterContext
// } from '@app/core';
import { WorkItemsService, FilterContext } from '../core/work-items/work-items.service';
import { WorkItem } from './work-item.model';

@Component({
  selector: 'app-work-items',
  templateUrl: './work-items.component.html',
  styleUrls: ['./work-items.component.css']
})
export class WorkItemsComponent implements OnInit {
  isLoading: boolean = true;
  page: number = 0;
  pages: number = 0;
  filterWorkItem: FilterContext;
  workItems: Array<WorkItem>;

  filterForm = new FormGroup({
    ctrlWorkItemTipo: new FormControl(''),
    ctrlWorkItemTitulo: new FormControl(''),
    ctrlWorkItemOrder: new FormControl('')
  });

  constructor(
    private workItemService: WorkItemsService,
  ) {}

  ngOnInit() {
    this.filter();
  }

  filter(linkPage: number = 0) {
    this.isLoading = true;
    this.filterWorkItem = {
      Tipo: this.filterForm.value.ctrlWorkItemTipo ? this.filterForm.value.ctrlWorkItemTipo : 0,
      Titulo: this.filterForm.value.ctrlWorkItemTitulo,
      Order: this.filterForm.value.ctrlWorkItemOrder,
      Pagina: linkPage
    };

    this.workItemService.ObterWorkItens(this.filterWorkItem).subscribe(result => {
      this.workItems = result;      
    });
  }
}
