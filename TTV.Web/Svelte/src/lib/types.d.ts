// ----- Common ------------------------------------------------------------------------------------

import { Page } from '@sveltejs/kit/src/exports/public';

export type ErrorState = {
  isError: boolean;
  message: string;
};

export type Lookup = {
  id: number;
  name: string;
};

export type OrderFilterState = {
  status: ?number;
  userId: ?string;
  isFinalised: boolean;
  from: ?Date;
  to: ?Date;
  skip: ?number;
  take: ?number;
};

export type PageInfo = {
  skip: number;
  take: number;
  count: number;
};

// ----- Entities ----------------------------------------------------------------------------------

export type AppliedDiscount = {
  amount: number;
  voucherBalance: number;
  voucherCode: string;
  usedAt: Date;
};

export type DiscountVoucher = {
  id: number;
  code: string;
  amount: number;
  balance: number;
  expirationDate: Date;
  note: ?string;
  claimedBy: ?User;
  issuedBy: User;
  issuedAt: Date;
};

export type Document = {
  id: number;
  title: string;
  documentType: Lookup;
};

export type Lesson = {
  id: number;
  title: string;
  description: string;
  lessonType: Lookup;
  isFree: boolean;
  tags: SimpleTag[];
  owner: User;
  currentPrice: Price;
  duration: string;
};

export type Order = {
  id: number;
  lessons: Lesson[];
  placedBy: User;
  placedOn: Date;
  status: Lookup;
  statusReason: ?string;
  orderTotal: number;
  paymentsTotal: number;
  totalAmount: number;
  appliedDiscounts: AppliedDiscount[];
  payments: Payment[];
  hasOwnedLessons: boolean;
  isFinalised: boolean;
  isPayable: boolean;
  canComplete: boolean;
};

export type OrderCreate = {
  lessonsIds: number[];
};

export type Payment = {
  id: string;
  paymentMethod: PaymentMethod;
  externalIdentifier: ?string;
  amount: number;
  status: Lookup;
  createdOn: Date;
};

export enum PaymentMethod {
  BankTransfer = 1,
  Payfast = 2,
  PayPal = 3
}

export type Price = {
  amount: number;
  promoAmount: number;
  effectiveAmount: number;
};

export type Result = {
  isSuccess: boolean;
  message: ?string;
};

export type ResultOf<T> = {
  isSuccess: boolean;
  message: ?string;
  value: ?T;
};

export type PageOf<T> = {
  items: T[];
  pageInfo: ?PageInfo;
};

export type SimpleTag = {
  name: string;
  priority: number;
};

export type Tag = {
  id: number;
  name: string;
  category: TagCategory;
  lessonCount: number;
};

export type TagCategory = {
  id: number;
  name: string;
  priority: number;
};

export type Testimonial = {
  seq: number;
  testimonialBy: string;
  text: string;
};

export type User = {
  id: string;
  name: string;
  provider: string;
  email: ?string;
};

// ----- Store States ------------------------------------------------------------------------------

export type AdminDiscountVouchersStoreState = {
  isLoading: boolean;
  pageInfo: ?PageInfo;
  discountVouchers: ?DiscountVoucher[];
  issuedVoucher: ?DiscountVoucher;
  error: ErrorState;
};

export type AdminOrdersStoreState = {
  isLoadingOrders: boolean;
  isLoadingUsers: boolean;
  isLoadingStatuses: boolean;
  pageInfo: ?PageInfo;
  orders: ?Order[];
  statuses: ?Lookup[];
  users: ?User[];
  filter: OrderFilterState;
  ordersError: ErrorState;
  statusesError: ErrorState;
  usersError: ErrorState;
};

export type AuthStoreState = {
  user: ?import('oidc-client').User;
  isAuthenticated: boolean;
  origin: 'login-callback' | 'user-loaded-event' | 'user-unloaded-event' | null;
  isLoading: boolean;
};

export type CheckoutStoreState = {
  isLoading: boolean;
  voucherCode: string;
  applyVoucherResult: ?ResultOf<?AppliedDiscount>;
  cancelOrderResult: ?ResultOf<Order>;
  initiatePaymentResult: ?ResultOf<?Payment>;
  order: ?Order;
  error: ErrorState;
};

export type LessonDetailStoreState = {
  isLoadingLesson: boolean;
  isLoadingDocuments: boolean;
  lesson: ?Lesson;
  documents: Document[];
  error: ErrorState;
};

export type LessonListStoreState = {
  isLoadingLessons: boolean;
  pageInfo: ?PageInfo;
  lessons: ?Lesson[];
  lessonsError: ErrorState;

  isLoadingTags: boolean;
  tags: ?Tag[];
  tagsError: ErrorState;

  isTagFilterDrawerOpen: boolean;
  searchText: ?string;
  searchTags: ?Tag[];
};

export type MyDiscountVouchersStoreState = {
  isLoading: boolean;
  pageInfo: ?PageInfo;
  discountVouchers: ?DiscountVoucher[];
  error: ErrorState;
};

export type MyLessonsStoreState = {
  isLoading: boolean;
  pageInfo: ?PageInfo;
  pageItems: ?Lesson[];
  lessons: ?Lesson[];
  error: ErrorState;
};

export type MyOrdersStoreState = {
  isLoading: boolean;
  pageInfo: PageInfo;
  orders: ?Order[];
  error: ErrorState;
};

export type OrderCompleteStoreState = {
  isLoading: boolean;
  completeOrderResult: ?ResultOf<Order>;
  order: ?Order;
  error: ErrorState;
};

export type ShoppingCartStoreState = {
  isLoading: boolean;
  lessons: Lesson[];
  error: ErrorState;
};

// ----- External ----------------------------------------------------------------------------------
declare global {
  interface Window {
    payfast_do_onsite_payment: (options: { uuid: string; return_url: string; cancel_url: string }) => void;
  }
}
