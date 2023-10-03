import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CategoryModel } from '../Models/CategoryModel';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private readonly url = environment.apiUrl + 'Category';

  constructor(private http: HttpClient) { }

  public getCategories(): Observable<CategoryModel[]> {
    return this.http.get<CategoryModel[]>(this.url);
  }

  public createCategory(category: CategoryModel): Observable<CategoryModel> {
    return this.http.post<CategoryModel>(this.url, category);
  }

  public updateCategory(categoryId:number, category: CategoryModel): Observable<CategoryModel> {
    return this.http.put<CategoryModel>(this.url + '/' + categoryId, category);
  }
  
  public deleteCategory(categoryId: number): Observable<CategoryModel> {
    return this.http.delete<CategoryModel>(this.url + '/' + categoryId);
  }

  public FindById(categoryId: number): Observable<CategoryModel> { 
    return this.http.get<CategoryModel>(this.url + '/' + categoryId);
  }
}
