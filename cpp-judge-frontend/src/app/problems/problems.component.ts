import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import * as monaco from 'monaco-editor';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-problems',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './problems.component.html',
  styleUrl: './problems.component.css'
})
export class ProblemsComponent implements OnInit {
  @ViewChild('editorContainer', { static: true }) editorContainer!: ElementRef;
  editor!: monaco.editor.IStandaloneCodeEditor;
  serverResponse = '';
  isLoading = false;
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.initializeEditor();
  }

  initializeEditor() {
    this.editor = monaco.editor.create(this.editorContainer.nativeElement, {
      value: '#include <iostream>\nusing namespace std;\nint main() {\n    \n    return 0;\n}',
      language: 'cpp',
      theme: 'vs-dark',
      automaticLayout: true,
      fontSize: 16,
      minimap: {
        enabled: false,
      },
      wordWrap: 'on', 
      wrappingIndent: 'indent',
    });
    
  }

  submitCode() {
    const codeContent = this.editor.getValue();
    const apiUrl = 'https://updatecc-latest-1.onrender.com/api/User';
    this.serverResponse = 'Waiting for judge...';
    this.http.post(apiUrl, { code: codeContent }).subscribe({
      next: (response: any) => {
        if(response.passed) {
          this.serverResponse = 'All test cases passed! ✅';
        } else {
          this.serverResponse = 'Wrong answer on test case ' + response.count + '❌\nInput: ' + response.failedTestCase.input + 'Expected output: ' + response.failedTestCase.expectedOutput + 'Your output: ' + response.actualOutput;
        }
      },
      error: (error) => {
        this.serverResponse = 'An error occurred: ' + error?.error?.error;
        this.isLoading = false;
      },
    });
  }
}
