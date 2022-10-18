import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { filter, takeUntil } from 'rxjs/operators';
import {  MsalBroadcastService } from '@azure/msal-angular';
import { InteractionStatus, PopupRequest, RedirectRequest } from '@azure/msal-browser';
import { AuthenticationService } from './services/authentication.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  isIframe = false;
  isLoggedIn = false;
  private readonly _destroying$ = new Subject<void>();
  constructor(

    private msalBroadcastService: MsalBroadcastService,
    private authenticationService: AuthenticationService
  ) { }

  ngOnInit(): void {
    this.initialLoading();
  }

  initialLoading() {
    this.isIframe = window !== window.parent && !window.opener;
    this.msalBroadcastService.inProgress$
      .pipe(
        filter((status: InteractionStatus) => status === InteractionStatus.None),
        takeUntil(this._destroying$)
      )
      .subscribe(() => {
        this.isLoggedIn = this.authenticationService.setLoginDisplay();
      });

  }

  login(userFlowRequest?: RedirectRequest | PopupRequest) {
    this.authenticationService.login(userFlowRequest);
  }

  logout() {
    this.authenticationService.logout();
  }


  ngOnDestroy(): void {
    this._destroying$.next(undefined);
    this._destroying$.complete();
  }
}
