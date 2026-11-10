import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {
searchForm: FormGroup;
@Input() messageStatuses: Record<number,string>|null;
@Output() search= new EventEmitter<number>();


constructor(private formBuilder: FormBuilder) { }



  ngOnInit(): void {
    this.searchForm = this.formBuilder.group({
      status:[]
    });
  }
  submitSearch() {
    const searchQuery: number =  +this.searchForm.controls['status'].value
    this.search.emit(searchQuery);
  }

   
}
