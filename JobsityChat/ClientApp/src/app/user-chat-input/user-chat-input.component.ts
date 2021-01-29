import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-user-chat-input-component',
  templateUrl: './user-chat-input.component.html'
})
export class UserChatInputComponent {
  public currentCount = 0;
  formGroup;

  constructor(
    private formBuilder: FormBuilder
  ) {
    this.formGroup = this.formBuilder.group({
      message: ''
    });
  }

  onSubmit(formData) {
    var name = formData['name'];
  }

  public incrementCounter() {
    this.currentCount++;
  }
}
