// ----- Common ------------------------------------------------------------------------------------

export type ErrorState = {
  isError: boolean;
  message: string;
};

export type Lookup = {
  id: number;
  name: string;
};

// ----- Entities ----------------------------------------------------------------------------------

export type Testimonial = {
  seq: number;
  testimonialBy: string;
  text: string;
};

export type TagCategory = {
  id: number;
  name: string;
  priority: number;
};

export type Tag = {
  id: number;
  name: string;
  category: TagCategory;
  lessonCount: number;
};

export type SimpleTag = {
  name: string;
  priority: number;
};

export type User = {
  id: string;
  email: ?string;
};

export type Price = {
  amount: number;
  promoAmount: number;
  effectiveAmount: number;
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

// ----- Store States ------------------------------------------------------------------------------

export type AuthStoreState = {
  user: import('oidc-client').User;
  isAuthenticated: boolean;
};

export type LessonListStoreState = {
  isLoadingLessons: boolean;
  lessons: ?Lesson[];
  lessonsError: ErrorState;

  isLoadingTags: boolean;
  tags: ?Tag[];
  tagsError: ErrorState;

  isTagFilterDrawerOpen: boolean;
  searchText: ?string;
  searchTags: ?Tag[];
};

export type ShoppingCartStoreState = {
  isLoading: boolean;
  lessons: Lesson[];
  error: ErrorState;
};

export type MyLessonsStoreState = {
  isLoading: boolean;
  lessons: ?Lesson[];
  error: ErrorState;
};
