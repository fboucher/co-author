
## Summary 

This project is a tool to help writing a blog post based on a video.
From a video (url or file), we will send the link to REKA Vision API and get a blog post draft in markdown format.

## Features / Requirements

- We need to be able to extract key frames from the video
- The app must be in a container
- We be to be able to upload a video file or provide a video url to Reka API
- we need to be able to manage the video in Reka's library (list, delete, get details)
- we need to be able to edit the generated blog post draft and save it for later
- We need to be able to export the blog post draft in markdown format, or HTML, with the image references embedded
- The App must have a nice and simple UI
- We need a setting page in the App to enter the Reka API key and other configurations

- The UI need to be webbased
- I want to use .NET 10.0
- Use mudblazor for the UI components
- I want an Aspire project 
- Use FFmpeg library for extract image frames from video

## Technical Details

video upload
Example with file upload with REKA API with a file upload:

```bash
curl -X POST https://vision-agent.api.reka.ai/v1/videos/upload \
  -H "X-Api-Key: YOUR_API_KEY" \
  -F "file=@video.mp4" \
  -F "video_name=my_video.mp4" \
  -F "index=true" \
  -F "group_id=20f4bc2d-3ebe-4fd2-829f-9d88c79e8a37"
```

Example with a URL upload with REKA API:

```bash
curl -X POST https://vision-agent.api.reka.ai/v1/videos/upload \
  -H "X-Api-Key: YOUR_API_KEY" \
  -F "video_url=https://www.youtube.com/watch?v=dQw4w9WgXcQ" \
  -F "video_name=my_video.mp4" \
  -F "index=true" \
  -F "group_id=default"
```

HEre how to list all the videos in Reka library:

```bash
curl -X GET "https://vision-agent.api.reka.ai/v1/videos" \
  -H "X-Api-Key: YOUR_API_KEY"
```

HEre how to delete video in Reka library:

```bash
curl -X DELETE https://vision-agent.api.reka.ai/v1/videos/550e8400-e29b-41d4-a716-446655440000 \
  -H "X-Api-Key: YOUR_API_KEY"
```

HEre how to ask Reka to generate a blog post draft from a video:

```bash
curl -X POST https://vision-agent.api.reka.ai/v1/qa/chat \
  -H "X-Api-Key: YOUR_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "video_id": "550e8400-e29b-41d4-a716-446655440000",
    "messages": [
      {
        "role": "user",
        "content": "Write a blog post draft in markdown format based on the content of the video."
      }
    ]
  }'

```