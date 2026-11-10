import { Component, OnInit } from '@angular/core';
import { map, Observable } from 'rxjs';
import { IMessage } from '../models/IMessage';
import { HttpService } from '../services/http-service.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MessageDetailsComponent } from '../message-details/message-details.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ISearch } from '../models/ISearch';


@Component({
  selector: 'app-main-view',
  templateUrl: './main-view.component.html',
  styleUrls: ['./main-view.component.css']
})
export class MainViewComponent implements OnInit {
  constructor(private httpService: HttpService, public dialog: MatDialog, private snackBar: MatSnackBar) { }
  messageList$: Observable<IMessage[]>;
  filteredAdList$: Observable<IMessage[]>;
  messageStatuses$: Observable<Record<number, string>>;
  curStatus: number;

  ngOnInit(): void {
    this.messageStatuses$ = this.httpService.getAllMessageStatuses();
  }


  openMessageDetails() {
    const dialogRef: MatDialogRef<MessageDetailsComponent, IMessage> = this.dialog.open(MessageDetailsComponent, {
      width: '750px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.httpService.addMessage(result).subscribe(response => this.validateResponse(response.status, 'הוספה'));
      }
    }
    );
  }


  private validateResponse(statusCode: number, processName: string) {
    if ([200, 201].includes(statusCode)) {
          this.messageList$ = this.httpService.getMessagesByStatus(this.curStatus);
      this.snackBar.open(`תהליך ה${processName} הושלם בהצלחה`, 'סגור');
    }
    else {
      this.snackBar.open(`חלה שגיאה בתהליך ה${processName}`, 'סגור');
    }
  }

  search(status: number) {
    this.curStatus = status;
    this.messageList$ = this.httpService.getMessagesByStatus(this.curStatus);
  }



}
