import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Notification {

  notification:string[];

  constructor(){
    this.notification=[
      "notify1",
      "notify2",
      "notify3",
      "notify4",
      // "",
      "notify5"
    ];
  }

getNotification():Observable<string>{
  return new Observable<string>((observer)=>{
      let index=0;
    let h=  setInterval(() => {
        if(index>=this.notification.length){
          return observer.complete();
          
        }
      let msg=this.notification[index];
      index++;
      
      if(msg==""){
        
        return observer.error("error occured");
      }
      return observer.next(msg);

    }, 2000);
    



return {
  unsubscribe:()=>{
    clearInterval(h);
  }

}


    }

  );
}









}
