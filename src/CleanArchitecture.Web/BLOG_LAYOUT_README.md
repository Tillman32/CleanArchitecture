# Blog Layout Documentation

## Overview

A clean, modern blog layout has been created for the CleanArchitecture.Web application. This layout features:

- **No Sidebar Navigation**: Clean header with top navigation bar
- **Featured Posts Section**: Sidebar widget area for promoting key content
- **Responsive Design**: Adapts beautifully from mobile to desktop
- **Tailwind CSS**: Utility-first styling for consistency

## Layout Structure

### Main Components

1. **Navigation Bar** (`Layout/NavMenu.razor`)
   - Horizontal navigation at the top
   - Links to Home, Blog, Counter, and Weather pages
   - Includes branding
   - Responsive design with proper spacing

2. **Main Layout** (`Layout/MainLayout.razor`)
   - Full-screen flex layout with footer
   - 3-column grid on large screens (2 main + 1 sidebar)
   - Single column on mobile devices
   - Footer with copyright information

3. **Sidebar Widgets**
   - **Featured Posts**: Latest/popular posts
   - **Categories**: Post categories with post counts
   - **About Widget**: Blog description and CTA

### Page Components

1. **Blog Listing Page** (`Pages/Blog.razor`)
   - Route: `/blog`
   - Displays multiple blog post summaries
   - Each post shows:
     - Title with link
     - Author name
     - Publication date
     - Read time estimate
     - Post excerpt
     - Category tags
   - Pagination controls

2. **Blog Post Detail Page** (`Pages/BlogPost.razor`)
   - Route: `/blog/{slug}`
   - Full blog post content
   - Author bio section
   - Related posts section
   - Comment form and existing comments

## Styling

### Color Scheme
- **Primary**: Blue (#3B82F6)
- **Background**: Light Gray (#F9FAFB)
- **Text**: Dark Gray (#111827)
- **Accents**: Various category colors

### Tailwind CSS Classes

Custom component classes available in `wwwroot/app.input.css`:

```css
.blog-post-meta        /* Metadata styling (author, date) */
.blog-post-tag         /* Base tag styling */
.blog-post-tag-primary /* Blue tag variant */
.blog-post-tag-secondary /* Green tag variant */
.blog-content h2/h3/p  /* Typography for blog content */
.card-hover            /* Hover effects for cards */
.featured-post         /* Gradient styling for featured posts */
```

## Usage

### Navigation
Add links to the blog pages by updating `NavMenu.razor`:

```razor
<NavLink class="text-gray-700 hover:text-blue-600 font-medium transition-colors" href="blog">
    Blog
</NavLink>
```

### Adding New Blog Posts
Update `Blog.razor` to add new post items to the list:

```razor
<article class="border-b border-gray-200 pb-8 last:border-b-0">
    <h2 class="text-2xl font-bold text-gray-900 mb-2">
        <a href="/blog/your-slug" class="hover:text-blue-600 transition-colors">Post Title</a>
    </h2>
    <!-- Post content -->
</article>
```

### Customizing Widgets
Edit the sidebar widgets in `MainLayout.razor`:

```razor
<aside class="lg:col-span-1">
    <!-- Add or modify widgets here -->
</aside>
```

## Responsive Behavior

| Screen Size | Layout |
|------------|--------|
| Mobile (< 768px) | Single column, widgets stack below content |
| Tablet (768px - 1024px) | 3-column grid begins |
| Desktop (> 1024px) | Full 3-column layout with optimized spacing |

## Future Enhancements

Suggested improvements:

1. **Data Integration**
   - Connect to a backend API for dynamic blog posts
   - Database support for post storage and retrieval

2. **Search Functionality**
   - Search bar in navigation
   - Full-text search across posts

3. **Category Filtering**
   - Dynamic category links
   - Filter posts by category

4. **User Interaction**
   - Like/share buttons
   - Comment system with moderation
   - Newsletter subscription

5. **Advanced Features**
   - Tags cloud widget
   - Recent comments widget
   - Social media integration
   - Reading time estimation
   - Table of contents for long posts

## File Structure

```
CleanArchitecture.Web/
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor (Updated)
│   │   └── NavMenu.razor (Updated)
│   └── Pages/
│       ├── Blog.razor (New)
│       ├── BlogPost.razor (New)
│       └── ...existing pages
├── wwwroot/
│   ├── app.input.css (Updated with blog styles)
│   └── css/
└── ...other files
```

## CSS Notes

All blog-specific styles use Tailwind's `@layer components` to ensure proper cascade and maintainability. This approach:

- Prevents style conflicts
- Makes overrides easier
- Keeps the stylesheet organized
- Leverages Tailwind's full utility system
