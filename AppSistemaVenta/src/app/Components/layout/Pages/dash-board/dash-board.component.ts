import { Component, OnInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { DashBoardService } from 'src/app/Services/dash-board.service';
Chart.register(...registerables);


@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.css']
})
export class DashBoardComponent implements OnInit {

  totalIngresos:string = '0';
  totalVentas:string = '0';
  totalProductos:string = '0';

  constructor(
    private _dashBoradServicio: DashBoardService
  ) { }

  mostrarGrafico(labelGrafico:any[], dataGrafico:any[]){
    const chartBarras = new Chart('chartBarras',{
      type:'bar',
      data:{
        labels:labelGrafico,
        datasets:[{
          label:'# de Ventas',
          data: dataGrafico,
          backgroundColor:['rgba(54,162,235,0.2)'],
          borderColor: ['rgba(54,162,235,1)'],
          borderWidth: 1
        }]
      },
      options:{
        maintainAspectRatio: false,
        responsive: true,
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }

  ngOnInit(): void {
    this._dashBoradServicio.resumen().subscribe({
      next:(data) => {
        if(data.status){
          console.log(data);
          this.totalIngresos = data.value.totalIngresos;
          this.totalVentas = data.value.totalVentas;
          this.totalProductos = data.value.totalProductos;

          const arrayData:any[] = data.value.ventasUltimasSemanas;
          console.log(arrayData);

          const labelTemp = arrayData.map((value) => value.fecha);
          const dataTemp = arrayData.map((value) => value.total);

          this.mostrarGrafico(labelTemp, dataTemp);
        }
      },
      error: (e) => {}
    })
  }



}
