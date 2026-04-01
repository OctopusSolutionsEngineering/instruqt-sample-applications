{{- define "demo-processor.name" -}}
demo-processor
{{- end }}

{{- define "demo-processor.fullname" -}}
{{ include "demo-processor.name" . }}
{{- end }}
