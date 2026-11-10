import { Component, Input, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { IMessage } from '../models/IMessage';

@Component({
  selector: 'app-messages-table',
  templateUrl: './messages-table.component.html',
  styleUrls: ['./messages-table.component.css']
})
export class MessagesTableComponent implements OnInit {
  @Input() messageList: IMessage[];
  displayedColumns: string[] = ['Id', 'WorkerName', 'WorkerAddress', 'Phone', 'Status', 'CreateTime', 'UpdateTime'];
  dataSource: MatTableDataSource<IMessage>;
  constructor() { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<IMessage>(this.messageList);
  }

}
