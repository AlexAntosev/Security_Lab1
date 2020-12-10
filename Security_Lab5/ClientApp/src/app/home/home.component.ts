import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { User } from '../_models';
import { AuthenticationService, UserService } from '../_services';


@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit, OnDestroy {
  currentUser: User;
  currentUserSubscription: Subscription;
  users: User[] = [];

  creditcard: string;

  cardForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
    private userService: UserService
  ) {
    this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
      this.currentUser = user;
    });
  }

  ngOnInit() {
    this.cardForm = this.formBuilder.group({
      creditcard: ['', Validators.required],
    });

    this.loadAllUsers();
  }

  get f() { return this.cardForm.controls; }

  ngOnDestroy() {
    // unsubscribe to ensure no memory leaks
    this.currentUserSubscription.unsubscribe();
  }

  deleteUser(id: number) {
    this.userService.delete(id).pipe(first()).subscribe(() => {
      this.loadAllUsers()
    });
  }

  postCard(username: string) {
    this.authenticationService.postCreditCard(username, this.f.creditcard.value)
      .subscribe();
  }

  getCard(username: string) {
    this.authenticationService.getCreditCard(username)
      .subscribe(c => this.creditcard = c);
  }

  private loadAllUsers() {
    this.userService.getAll().pipe(first()).subscribe(users => {
      this.users = users;
    });
  }
}
