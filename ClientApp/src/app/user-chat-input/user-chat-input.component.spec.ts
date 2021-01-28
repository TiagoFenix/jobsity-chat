import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserChatInputComponent } from './user-chat-input.component';

describe('UserChatInputComponent', () => {
  let component: UserChatInputComponent;
  let fixture: ComponentFixture<UserChatInputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [UserChatInputComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserChatInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

});
