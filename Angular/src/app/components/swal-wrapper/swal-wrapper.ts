import { Component, OnInit, OnDestroy, ViewChild, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SweetAlert2Module, SwalComponent, SwalDirective } from '@sweetalert2/ngx-sweetalert2';
import { Subscription } from 'rxjs';
import { SweetAlertIcon } from 'sweetalert2';
import { AlertCenter, AlertMessage, AlertPriority } from '../../services/alert-center';
@Component({
  selector: 'app-swal-wrapper',
  imports: [SwalComponent],
  templateUrl: './swal-wrapper.html',
  styleUrl: './swal-wrapper.css',
})
export class SwalWrapper {
  private subscription!: Subscription;
  @ViewChild('alertSwal') alertSwal!: SwalComponent;
  // Alert State
  title = '';
  text = '';
  icon: SweetAlertIcon = 'info';
  confirmButtonColor = '#0d6efd';
  confirmButtonText = 'OK';

  // Priority to Style Mapping
  private priorityConfig: Record<AlertPriority, { icon: SweetAlertIcon; color: string }> = {
    error: { icon: 'error', color: '#dc3545' },       // Red
    warning: { icon: 'warning', color: '#fd7e14' },   // Orange
    info: { icon: 'info', color: '#0d6efd' },         // Blue
    success: { icon: 'success', color: '#198754' }    // Green
  };

  constructor(private alertCenter: AlertCenter, private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    this.subscription = this.alertCenter.alert$.subscribe((data: AlertMessage) => {
      this.triggerSwal(data);
      console.log(data.message);
    });
  }

  private triggerSwal(data: AlertMessage) {
    const config = this.priorityConfig[data.priority] || this.priorityConfig.info;

    this.title = data.title;
    this.text = data.message;
    this.icon = config.icon;
    this.confirmButtonColor = config.color;
    this.confirmButtonText = data.confirmButtonText || 'OK';
    this.cdr.detectChanges();
    // Fire SweetAlert
    setTimeout(() => {
      this.alertSwal.fire();
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
