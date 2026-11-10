import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { IMessage } from '../models/IMessage';
import { HttpService } from '../services/http-service.service';
import { Observable, tap } from 'rxjs';

@Component({
  selector: 'app-message-details',
  templateUrl: './message-details.component.html',
  styleUrls: ['./message-details.component.css']
})
export class MessageDetailsComponent implements OnInit {
  prefixes: string[] = [];
  messageForm: FormGroup;
  constructor(private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<MessageDetailsComponent>,
    //@Inject(MAT_DIALOG_DATA) public data: IMessage,
    private httpService: HttpService,
  ) {
    [...Array(10)].forEach((e, i) => this.prefixes.push(`05${i}`));
  }



  ngOnInit(): void {
    this.messageForm = this.formBuilder.group({
      FirstName: [, [Validators.required, Validators.maxLength(15)]],
      LastName: [, [Validators.required, Validators.maxLength(15)]],
      Content: [, [Validators.required, Validators.minLength(5)]],
      City: [, [Validators.required,Validators.minLength(2), Validators.maxLength(30)]],
      Street: [, [Validators.required,Validators.minLength(2), Validators.maxLength(30)]],
      HouseNumber: [, [Validators.required, Validators.min(1), Validators.maxLength(3)]],
      PhonePrefix: [, [Validators.required,Validators.minLength(3), Validators.maxLength(3)]],
      PhoneNumber: [,[Validators.required,Validators.minLength(7), Validators.maxLength(7)]],
    });
  }
  submitMessage() {
    const savedMessage: IMessage = {
      Worker: {
        FirstName: this.messageForm.controls['FirstName'].value,
        LastName: this.messageForm.controls['LastName'].value,
        Address: {
          City: this.messageForm.controls['City'].value,
          Street: this.messageForm.controls['Street'].value,
          HouseNumber: Number(this.messageForm.controls['HouseNumber'].value)
        }
      },
      Content: this.messageForm.controls['Content'].value,
      Phone: {
        Prefix: this.messageForm.controls['PhonePrefix'].value,
        Number: this.messageForm.controls['PhoneNumber'].value
      },
    }

    this.dialogRef.close(savedMessage);
  }


  numberKeyPress(event:KeyboardEvent){
    if(isNaN(Number(event.key)))
      event.preventDefault()
  }

}