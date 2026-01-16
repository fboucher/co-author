# Co-Author Project Development Plan

## Overview
A .NET 10.0 Aspire application that converts videos into blog post drafts using the Reka Vision API, with frame extraction, editing, and export capabilities.

---

## Phase 1: Project Setup & Infrastructure

### 1.1 Aspire Project Structure
- [ ] Create .NET 10.0 Aspire project with proper structure:
  - `CoAuthor.AppHost` - Aspire orchestration project
  - `CoAuthor.ServiceDefaults` - Shared configuration
  - `CoAuthor.Web` - Blazor web frontend
  - `CoAuthor.Api` - Backend API (if needed for separation)
  - `CoAuthor.Services` - Business logic (video processing, Reka integration)

### 1.2 Docker & Container Setup (via Aspire)
- [ ] Create Dockerfile for containerized deployment
- [ ] Include FFmpeg in the container
- [ ] Use Aspire's container orchestration to manage docker-compose
- [ ] Define volume mounts in Aspire:
  - [ ] `/posts` - Blog post drafts (mounted folder)d
  - [ ] `/images` - Extracted video frames (mounted folder)
- [ ] Configure Aspire services in `CoAuthor.AppHost` project
- [ ] Set up environment variables through Aspire

### 1.3 Development Environment
- [ ] Set up local development configuration
- [ ] Create appsettings files (development, production)
- [ ] Configure logging and diagnostics

---

## Phase 2: Frontend Development (Blazor + MudBlazor)

### 2.1 Core UI Structure
- [ ] Set up Blazor Server components with MudBlazor
- [ ] Create main layout with navigation
- [ ] Implement responsive design for desktop/tablet

### 2.2 Settings Page
- [ ] Build settings page component
- [ ] Create form to input Reka API key
- [ ] Save API key to appsettings or secure storage
- [ ] Display current configuration status

### 2.3 Video Upload Page
- [ ] File upload component (local file)
- [ ] URL input component (for video links)

### 2.4 Video Library/Management Page
- [ ] Display list of videos in Reka library
- [ ] Show video details (name, size, upload date, thumbnail,etc.)
- [ ] Implement delete functionality with confirmation
- [ ] Add refresh/sync button

### 2.5 Blog Draft Editor Page
- [ ] Markdown editor component
- [ ] Live preview pane
- [ ] Draft save/auto-save functionality
- [ ] Edit existing draft capability

### 2.6 Export Page
- [ ] Export to markdown format
- [ ] Export to HTML format
- [ ] Option to embed images in export
- [ ] Download/file save dialog

---

## Phase 3: Backend Services

### 3.1 Reka API Integration Service (using Reka.SDK)
- [ ] Install `Reka.SDK` NuGet package
- [ ] Create `RekaVideoService` class
  - [ ] Upload video file using Reka.SDK
  - [ ] Upload video from URL using Reka.SDK
  - [ ] List videos from Reka library
  - [ ] Delete video from Reka library
  - [ ] Get video details/metadata
- [ ] Create `RekaAiService` class
  - [ ] Generate blog post draft using Reka QA endpoint
  - [ ] Handle chat/QA requests with video context
- [ ] Configure HttpClient with Reka API base URL
- [ ] Add API key validation and error handling
- [ ] Implement retry logic for failed requests

### 3.2 Video Processing Service
- [ ] Create `VideoFrameExtractor` service
  - [ ] Use FFmpeg to extract key frames
  - [ ] Configure frame extraction parameters (time, quality)
  - [ ] Return frame file paths
- [ ] Temporary storage management for extracted frames

### 3.3 Export Service
- [ ] Create `ExportService` class
  - [ ] Export draft as markdown file
  - [ ] Convert markdown to HTML using `Markdig` NuGet package
  - [ ] Generate file downloads
  - [ ] Handle image reference paths in exports

### 3.4 Blog Draft Management Service
- [ ] Create `BlogDraftService` class
  - [ ] Save draft as markdown file to `/posts` folder
  - [ ] Load draft from `/posts` folder
  - [ ] Update/edit draft file
  - [ ] List all drafts from `/posts` folder
  - [ ] Delete draft file
- [ ] Implement file-based storage with markdown files

### 3.5 Image/Asset Management Service
- [ ] Create `FrameExtractorService` for FFmpeg integration
- [ ] Handle extracted frame storage in `/images/{videoId}/` folders
- [ ] Generate markdown image references (relative paths)
- [ ] Clean up old frame folders when videos are deleted

---

## Phase 4: Data & Storage

### 4.1 File Structure
- [ ] Create and manage mounted volumes:
  - `/posts` - Saved blog post drafts (markdown files)
  - `/images` - Extracted video frames organized by video ID

### 4.2 Configuration Storage
- [ ] Store API key in appsettings.json or secure configuration
- [ ] Store user preferences (if any) in configuration files

### 4.3 Metadata Tracking
- [ ] Create simple JSON metadata files for drafts (optional)
  - Title, creation date, associated video ID, last modified

---

## Phase 5: API/Service Layer (Blazor Server Backend)

### 5.1 Service Methods & Event Handlers
- [ ] `VideoUploadService.UploadFileAsync()` - Handle file upload
- [ ] `VideoUploadService.UploadUrlAsync()` - Handle URL upload
- [ ] `VideoManagementService.ListVideosAsync()` - Get Reka library
- [ ] `VideoManagementService.DeleteVideoAsync()` - Remove from Reka
- [ ] `BlogGenerationService.GenerateDraftAsync()` - Call Reka AI
- [ ] `DraftEditorService.LoadDraftAsync()` - Read from files
- [ ] `DraftEditorService.SaveDraftAsync()` - Write to files
- [ ] `ExportService.ExportToMarkdown()` - Export with references
- [ ] `ConfigurationService.GetApiKey()` - Retrieve API key
- [ ] `ConfigurationService.SetApiKey()` - Store API key

---

## Phase 6: Integration & Workflows

### 6.1 Video Upload Workflow
- [ ] Receive file/URL from UI
- [ ] Validate video format
- [ ] Call Reka API to upload
- [ ] Store metadata locally
- [ ] Extract key frames with FFmpeg
- [ ] Return video info to UI

### 6.2 Blog Generation Workflow
- [ ] Receive video selection from UI
- [ ] Call Reka QA endpoint with blog prompt
- [ ] Parse response (markdown)
- [ ] Save as draft
- [ ] Return to UI for editing

### 6.3 Draft Edit & Save Workflow
- [ ] Receive edits from UI
- [ ] Save to storage
- [ ] Maintain version history (optional)

### 6.4 Export Workflow
- [ ] Format blog content (markdown/HTML)
- [ ] Process image references
- [ ] Embed frames if requested
- [ ] Generate downloadable file

---

## Phase 7: Security & Configuration

### 7.1 API Key Management
- [ ] Store API key in appsettings or environment variables
- [ ] Never log or expose API key in errors
- [ ] Validate API key on startup
- [ ] Display masked key in settings UI for confirmation

### 7.2 Input Validation
- [ ] Validate file uploads (video format: mp4, mov, mkv, etc.)
- [ ] Validate video duration (max 90 minutes)
- [ ] Validate URLs for video links
- [ ] Sanitize markdown content in drafts

### 7.3 CORS & Security Headers
- [ ] Configure CORS if needed
- [ ] Add security headers
- [ ] Implement HTTPS in production

---

## Phase 8: Testing & Quality

### 8.1 Unit Tests
- [ ] Test Reka API service
- [ ] Test video frame extraction
- [ ] Test blog draft management
- [ ] Test export formatting

### 8.2 Integration Tests
- [ ] Test full workflows
- [ ] Test file operations
- [ ] Test API endpoints

### 8.3 UI Testing (Manual/E2E)
- [ ] Test upload flow
- [ ] Test settings page
- [ ] Test editor functionality
- [ ] Test export downloads

---

## Phase 9: Deployment & Documentation

### 9.1 Docker & Container Orchestration
- [ ] Build and test Docker image
- [ ] Set up container registry
- [ ] Document container startup

### 9.2 Production Configuration
- [ ] Configure Aspire for production
- [ ] Set up logging/monitoring
- [ ] Configure environment variables

### 9.3 Documentation
- [ ] API documentation
- [ ] User guide
- [ ] Setup instructions
- [ ] Architecture documentation

---

## Clarifications & Decisions

1. **Storage Backend**: Flat file system (no database)
2. **Authentication**: Single-user, no login required
3. **Frame Storage**: Generate on-demand and save to images subfolder
4. **API Integration**: Use `Reka.SDK` NuGet package with HttpClient
5. **Image Handling**: Markdown references only (no embedding/base64)
6. **Video Limits**: No file size limit, but max 90 minutes duration
7. **API Key Management**: User provides via settings page

---

## File Structure (Mounted Volumes)
```
/app-data/
├── posts/              # Saved blog post drafts (markdown files)
│   ├── draft-1.md
│   ├── draft-2.md
│   └── ...
└── images/             # Extracted video frames
    ├── video-id-1/
    │   ├── frame-001.jpg
    │   ├── frame-002.jpg
    │   └── ...
    └── video-id-2/
        ├── frame-001.jpg
        └── ...
```

---

## Technology Stack Summary
- **Framework**: .NET 10.0 with Aspire
- **Frontend**: Blazor Server + MudBlazor
- **Backend**: ASP.NET Core Services
- **Video Processing**: FFmpeg
- **Markdown Conversion**: Markdig (markdown to HTML)
- **External API**: Reka Vision API (via `Reka.SDK` NuGet)
- **HTTP Client**: HttpClient with Reka.SDK
- **Container**: Docker
- **Storage**: File system with mounted volumes (posts & images)
