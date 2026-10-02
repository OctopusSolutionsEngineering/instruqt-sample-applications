{{- define "demo-api.name" -}}
demo-api
{{- end }}

{{- define "demo-api.fullname" -}}
{{ include "demo-api.name" . }}
{{- end }}
