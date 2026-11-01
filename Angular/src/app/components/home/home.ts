import { Component, OnDestroy, OnInit } from '@angular/core';
import { Notification } from '../../services/notification';
import { Subscription } from 'rxjs';
import { RouterLink } from '@angular/router';
import { NgModel } from '@angular/forms';
import { CommonModule, NgClass } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [RouterLink,NgClass,CommonModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit,OnDestroy {

   subscription!:Subscription;

isDarkTheme = false;



  toggleTheme() {
    this.isDarkTheme = !this.isDarkTheme;
  }
  constructor(private notificationService:Notification){

  }
  ngOnDestroy(): void {
       
    this.subscription.unsubscribe();

  }
  ngOnInit(): void {
    
    // this.notificationService.getNotification().subscribe((msg)=>{
    //   console.log(msg);
    // },(error)=>{
    //   console.log(`-- {error}`);

    // });



    this.subscription=this.notificationService.getNotification().subscribe(
      {
        
        next:(msg)=>{
      console.log(msg);
        },
        error:(msg)=>{
          console.log(msg);
        },
        complete:()=>{
          console.log("complete");
        },
          
      });
      
  }






  projectName = 'Inventory Management';

  githubUrl =
    'https://github.com/Hussien-A-Amin/InventoryManagement-CleanArchitecture';

  technologies = [
    'C#',
    '.NET',
    'ASP.NET Core Web API',
    'Entity Framework Core',
    'SQL Server',
    'JWT'
  ];

  features = [
    {
      icon: 'fa-sitemap',
      title: 'Clean Architecture',
      description:
        'Separate business rules, application logic, infrastructure, and API endpoints to improve maintainability and testability.'
    },
    {
      icon: 'fa-shield-alt',
      title: 'Authentication & Authorization',
      description:
        'Use ASP.NET Core Identity and JWT Bearer authentication to establish identity and protect API resources.'
    },
    {
      icon: 'fa-exclamation-triangle',
      title: 'Error Handling',
      description:
        'Design consistent API responses, validate requests, and handle failures without exposing internal implementation details.'
    },
    {
      icon: 'fa-database',
      title: 'Data Access',
      description:
        'Use Entity Framework Core and SQL Server for structured relational data access and persistence.'
    },
    {
      icon: 'fa-vial',
      title: 'Software Quality',
      description:
        'Explore automated testing and validation to improve application reliability and reduce regressions.'
    },
    {
      icon: 'fa-chart-line',
      title: 'Observability & Performance',
      description:
        'Explore structured logging, telemetry, monitoring, and caching to improve application visibility and performance.'
    }
  ];

  architectureLayers = [
    {
      number: '04',
      title: 'API Layer',
      icon: 'fa-globe',
      description:
        'Controllers, HTTP requests, middleware, and dependency injection configuration.'
    },
    {
      number: '03',
      title: 'Application Layer',
      icon: 'fa-cogs',
      description:
        'Application use cases, DTOs, interfaces, and application-level validation.'
    },
    {
      number: '02',
      title: 'Domain Layer',
      icon: 'fa-cube',
      description:
        'Core business entities and domain rules independent of infrastructure.'
    },
    {
      number: '01',
      title: 'Infrastructure Layer',
      icon: 'fa-server',
      description:
        'Database persistence, Entity Framework Core, identity infrastructure, and external service implementations.'
    }
  ];

  technologyGroups = [
    {
      title: 'Backend Development',
      icon: 'fa-code',
      items: [
        'C#',
        '.NET',
        'ASP.NET Core Web API',
        'REST',
        'Dependency Injection'
      ]
    },
    {
      title: 'Data & Persistence',
      icon: 'fa-database',
      items: [
        'SQL Server',
        'Entity Framework Core',
        'LINQ',
        'Migrations'
      ]
    },
    {
      title: 'Security',
      icon: 'fa-lock',
      items: [
        'ASP.NET Core Identity',
        'JWT Bearer',
        'Authentication',
        'Authorization'
      ]
    },
    {
      title: 'Testing & Quality',
      icon: 'fa-vial',
      items: [
        'Unit Testing',
        'Integration Testing',
        'Validation'
      ]
    },
    {
      title: 'Observability',
      icon: 'fa-chart-bar',
      items: [
        'Serilog',
        'OpenTelemetry',
        'Grafana',
        'Application Monitoring'
      ]
    },
    {
      title: 'Performance & Delivery',
      icon: 'fa-rocket',
      items: [
        'Redis',
        'Git',
        'GitHub',
        'CI/CD'
      ]
    }
  ];

  entities = [
    {
      icon: 'fa-users',
      title: 'Customers',
      description:
        'Represent customers and demonstrate validation, persistence, and customer-related application use cases.'
    },
    {
      icon: 'fa-box-open',
      title: 'Products',
      description:
        'Represent products and demonstrate resource creation, retrieval, and input validation.'
    },
    {
      icon: 'fa-shopping-cart',
      title: 'Orders',
      description:
        'Represent orders associated with customers and products.'
    }
  ];

  roadmap = [
    {
      icon: 'fa-memory',
      title: 'Caching & Performance',
      description:
        'Explore Redis caching and measure improvements for suitable read-heavy operations.'
    },
    {
      icon: 'fa-chart-line',
      title: 'Logging & Observability',
      description:
        'Explore Serilog, OpenTelemetry, and Grafana.'
    },
    {
      icon: 'fa-vial',
      title: 'Automated Testing',
      description:
        'Expand automated tests for application use cases and API behavior.'
    },
    {
      icon: 'fa-sync-alt',
      title: 'CI/CD Automation',
      description:
        'Explore automated builds and test execution with GitHub Actions.'
    }
  ];

}




