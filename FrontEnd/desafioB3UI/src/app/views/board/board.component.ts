import { Component, OnInit } from '@angular/core';
import { NgFor } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  CdkDrag,
  CdkDragDrop,
  CdkDropList,
  CdkDropListGroup,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { Column } from 'src/app/models/column.model';
import { Card } from 'src/app/models/card.model';
import { BoardService } from 'src/app/services/board.service';
import { BaseResponse } from 'src/app/models/response/base_response.model';

@Component({
  selector: 'app-board',
  templateUrl: 'board.component.html',
  styleUrls: ['board.component.css'],
  standalone: true,
  imports: [
    CdkDropListGroup,
    CdkDropList,
    NgFor,
    CdkDrag,
    ReactiveFormsModule,
    FormsModule,
  ],
})
export class BoardComponent implements OnInit {
  editedCard: Card | undefined; // Objeto card a ser editado
  columns: Column[] = [];
  dataForm: FormGroup;
  editForm: FormGroup;
  showEditModal = false;

  constructor(private _service: BoardService, private fb: FormBuilder) {
    this.dataForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
    });

    this.editForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.getBoard();
  }

  getBoard() {
    this._service.board().subscribe((response: BaseResponse) => {
      this.columns = response.data as Column[];
    });
  }

  addItem() {
    const card = new Card(
      0,
      0,
      this.dataForm!.get('name')!.value,
      this.dataForm!.get('description')!.value,
      new Date()
    );

    this._service.addItem(card).subscribe((response: BaseResponse) => {
      this.getBoard();
    });
  }

  drop(event: CdkDragDrop<Card[]>, columnId: number) {
    const card = event.previousContainer.data[event.previousIndex] as Card;
    card.statusId = columnId;

    this._service.updateItem(card).subscribe(
      () => {
        if (event.previousContainer === event.container) {
          moveItemInArray(
            event.container.data,
            event.previousIndex,
            event.currentIndex
          );
        } else {
          transferArrayItem(
            event.previousContainer.data,
            event.container.data,
            event.previousIndex,
            event.currentIndex
          );
        }
      },
      (err: BaseResponse) => {
        console.log('aconteceu um erro');
      }
    );
  }
}
