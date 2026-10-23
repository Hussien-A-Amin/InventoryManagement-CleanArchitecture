import { Component } from '@angular/core';
import { AlertCenter } from '../../../services/alert-center';

@Component({
  selector: 'app-about',
  imports: [],
  templateUrl: './about.html',
  styleUrl: './about.css',
})
export class About {
  constructor(private alertCenter: AlertCenter) {}
  triggerError() {
    
    this.alertCenter.error('Database Error', 'Unable to connect to the server. Please try again.');
  }

  triggerWarning() {
    this.alertCenter.warning('Session Expiring', 'Your session will expire in 5 minutes.');
  }

  triggerInfo() {
    this.alertCenter.info('Update Available', 'A new software update is ready to install.');
  }
  triggersuccess() {
    this.alertCenter.success('Changes Saved!', 'Your profile has been updated successfully.');

  }
}
