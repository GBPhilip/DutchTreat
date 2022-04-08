﻿import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Order, OrderItem } from "../shared/Order";
import { Product } from "../shared/Product";
@Injectable()

export class Store {

    constructor(private http: HttpClient) {

    }

    public products: Product[] = [];

    public order: Order = new Order();

    loadProducts(): Observable<void> {
        return this.http.get<[]>("/api/products")
            .pipe(map(data => {
                this.products = data;
            }))
    }
    addToOrder(product: Product) {
        const newItem = new OrderItem();
        newItem.productId = product.id;
        newItem.productArtId = product.artId;
        newItem.productArtist = product.artist;
        newItem.productCategory = product.category;
        newItem.productSize = product.size;
        newItem.productTitle = product.title;
        newItem.unitPrice = product.price;
        newItem.quantity = 1;

        this.order.items.push(newItem)
    }
}