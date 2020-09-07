import { AttendeeService } from '../../services/attendee/attendee.service';
import { Component, OnInit, Inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import * as XLSX from 'xlsx';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

  itemId: string;
  loading = false;
  file: File;
  arrayBuffer: any;

  constructor(
    private snackBar: MatSnackBar,
    private attendeeService: AttendeeService,
    private dialogRef: MatDialogRef<UploadComponent>,
    @Inject(MAT_DIALOG_DATA) private data: string) {
      this.itemId = data;
    }

  ngOnInit(): void {
  }

  upload(event: any): void {
    this.file = event.target.files[0];
  }

  onSubmit(): void {
    this.loading = true;
    const fileReader = new FileReader();
    fileReader.onload = (e) => {
      this.arrayBuffer = fileReader.result;
      const data = new Uint8Array(this.arrayBuffer);
      const arr = new Array();
      for (let i = 0; i !== data.length; ++i) { arr[i] = String.fromCharCode(data[i]); }
      const bstr = arr.join('');
      const workbook = XLSX.read(bstr, {type: 'binary'});
      const firstSheetName = workbook.SheetNames[0];
      const worksheet = workbook.Sheets[firstSheetName];
      this.attendeeService
        .createAttendeesForItemId(this.itemId, XLSX.utils.sheet_to_json(worksheet, {header: ['nid', 'department', 'name'], raw: true}))
        .subscribe(
          data => {
            this.snackBar.open('新增成功', '關閉', { duration: 5000 });
            this.loading = false;
            this.dialogRef.close();
          },
          error => {
            this.snackBar.open('發生錯誤', '關閉', { duration: 5000 });
            this.loading = false;
          }
        );
    };
    fileReader.readAsArrayBuffer(this.file);
  }

}
