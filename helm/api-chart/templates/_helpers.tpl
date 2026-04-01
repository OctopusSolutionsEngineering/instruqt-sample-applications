{{- define "demo-api.name" -}}
demo-frontend
{{- end }}

{{- define "demo-api.fullname" -}}
{{ include "demo-api.name" . }}
{{- end }}
