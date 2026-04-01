{{- define "demo-frontend.name" -}}
demo-frontend
{{- end }}

{{- define "demo-frontend.fullname" -}}
{{ include "demo-frontend.name" . }}
{{- end }}
