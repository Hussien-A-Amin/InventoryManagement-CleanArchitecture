import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export type AlertPriority = 'error' | 'warning' | 'info' | 'success';

export interface AlertMessage {
  priority: AlertPriority;
  title: string;
  message: string;
  confirmButtonText?: string;
}
@Injectable({
  providedIn: 'root',
})
export class AlertCenter {
private alertSubject = new Subject<AlertMessage>();
  public alert$ = this.alertSubject.asObservable();


// Primary trigger method
  showAlert(priority: AlertPriority, title: string, message: string, confirmButtonText = 'OK') {
    this.alertSubject.next({ priority, title, message, confirmButtonText });
  }

  // Helper methods for convenience
  error(title: string, message: string) {
    this.showAlert("error", title, message);
  }

  warning(title: string, message: string) {
    this.showAlert("warning", title, message);
  }

  info(title: string, message: string) {
    this.showAlert("info", title, message);
  }

  success(title: string, message: string) {
    this.showAlert("success", title, message);
  }
}
