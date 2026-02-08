export interface Product {
  productId: number;
  categoryId: number;
  productName: string;
  description?: string;
  price: number;
  discountPrice?: number;
  stockQuantity: number;
  unit: string;
  imageUrl?: string;
  brand?: string;
  isActive: boolean;
  isFeatured: boolean;
  categoryName?: string;
}

export interface ProductDetail extends Product {
  category?: Category;
}

export interface Category {
  categoryId: number;
  categoryName: string;
  description?: string;
  imageUrl?: string;
  isActive: boolean;
}
