import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { Observable, timer } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Component({
  selector: 'app-user-chat',
  templateUrl: './user-chat.component.html'
})
export class UserChatComponent {
  public messages: ChatMessage[];
  private http: HttpClient
  private baseUrl: string;
  public userName: string;
  formGroup;
  interval;

  ngOnInit() {
    this.authorizeService.getUser().pipe(map(u => u && u.name)).subscribe(x => { this.userName = x; });

    this.loadData().subscribe(response => { this.messages = response; });
    this.interval = setInterval(() => {
      this.loadData().subscribe(response => { this.messages = response; });
    }, 5000);
  }

  ngOnDestroy() {
    if (this.interval) {
      clearInterval(this.interval);
    }
  }


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private formBuilder: FormBuilder, private authorizeService: AuthorizeService) {
    this.http = http;
    this.baseUrl = baseUrl;

    this.formGroup = this.formBuilder.group({
      message: ''
    });
  }

  loadData() {
    return this.http.get<ChatMessage[]>(this.baseUrl + 'api/chatmessage/v1/last', { responseType: 'json', reportProgress: true }).pipe(
      map(response => response)
    );
  }

  onSubmit(form) {
    form["CreatedBy"] = this.userName;
    this.http.post<ChatMessage>(this.baseUrl + 'api/chatmessage/v1', form).subscribe(
      (response) => { console.log(response); this.formGroup.reset(); this.loadData().subscribe(response => { this.messages = response; });},
      (error) => console.log(error)
    );
  }
}

interface ChatMessage {
  id: number;
  message: string;
  created: Date;
  createdBy: string;
}
